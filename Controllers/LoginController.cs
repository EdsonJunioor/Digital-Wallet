using DigitalWallet.DTO;
using DigitalWallet.Interface;
using DigitalWallet.Services;
using Microsoft.AspNetCore.Mvc;

namespace DigitalWallet.Controllers
{
    [ApiController]
    [Route("api")]
    public class LoginController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtService _jwtService;

        public LoginController(IUserRepository userRepository, JwtService jwtService)
        {
            _userRepository = userRepository ?? throw new Exception("repositorio nao encontrado");
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<UserDto> Login([FromBody] LoginDto loginDto)
        {
            if (loginDto == null || string.IsNullOrWhiteSpace(loginDto.Email) || string.IsNullOrWhiteSpace(loginDto.Password))
            {
                throw new ArgumentException("Login e senha são obrigatórios.");
            }

            var user = await _userRepository.GetUserByEmailAsync(loginDto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            {
                throw new UnauthorizedAccessException("Login ou senha inválidos, tente novamente.");
            }

            var token = _jwtService.GenerateToken(user.Id.ToString(), user.Email);

            return new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Token = token
            };
        }

        [HttpPost("recuperar-senha")]
        public async Task<IActionResult> RecoverPassword([FromBody] LoginDto input)
        {
            if (input == null || string.IsNullOrWhiteSpace(input.Email) || string.IsNullOrWhiteSpace(input.Password))
            {
                throw new ArgumentException("Login e senha são obrigatórios.");
            }

            var user = await _userRepository.GetUserByEmailAsync(input.Email);

            if ((!string.IsNullOrEmpty(input.Password)) && user.Password != input.Password)
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(input.Password);
                _userRepository.Update(user);
            }
            else
            {
                return BadRequest("Não foi possível alterar a senha.");
            }

            return Ok("Senha atualizada com sucesso!");
        }
    }
}
