using AutoMapper;
using DigitalWallet.DTO;
using DigitalWallet.Enum;
using DigitalWallet.Interface;
using DigitalWallet.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DigitalWallet.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new Exception("Repository Not Found!");
            _mapper = mapper;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] UserDto input)
        {
            try
            {
                var checkEmailExists = await _userRepository.GetUserByEmailAsync(input.Email ?? string.Empty);
                if (checkEmailExists != null)
                {
                    throw new Exception("Este email já foi usado, tente novamente com um email válido.");
                }
                var user = _mapper.Map<User>(input);
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                _userRepository.Create(user);
                return Ok("Usuário foi criado com sucesso !");
            }
            catch (Exception e)
            {
                return BadRequest("Erro ao criar usuário.");
            }
        }

        [Authorize]
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UserDto input)
        {
            try
            {
                if (input.Id == 0)
                {
                    return Ok();
                }
                else
                {
                    var user = await _userRepository.GetById((int)input.Id!);

                    if (user == null)
                        return BadRequest("Usuário não encontrado");

                    bool hasChanges = false;

                    if ((!string.IsNullOrEmpty(input.Name)) && user.Name != input.Name)
                    {
                        user.Name = input.Name;
                        hasChanges = true;
                    }

                    if ((!string.IsNullOrEmpty(input.Email)) && user.Email != input.Email)
                    {
                        user.Email = input.Email;
                        hasChanges = true;
                    }

                    if ((!string.IsNullOrEmpty(input.Password)) && user.Password != input.Password)
                    {
                        user.Password = BCrypt.Net.BCrypt.HashPassword(input.Password);
                        hasChanges = true;
                    }

                    if ((!string.IsNullOrEmpty(input.CPF)) && user.CPF != input.CPF)
                    {
                        user.Password = input.CPF;
                        hasChanges = true;
                    }

                    if (input.Bank != null && Convert.ToInt32(user.Bank) != input.Bank)
                    {
                        user.Password = input.Password;
                        hasChanges = true;
                    }

                    if ((!string.IsNullOrEmpty(input.WalletNumber)) && user.WalletNumber != input.WalletNumber)
                    {
                        user.Password = input.WalletNumber;
                        hasChanges = true;
                    }

                    if (hasChanges)
                        _userRepository.Update(user);
                }

                return Ok("Usuário atualizado com sucesso!");

            }
            catch (Exception e)
            {
                return BadRequest("Erro ao editar usuário.");
            }

        }
    }
}
