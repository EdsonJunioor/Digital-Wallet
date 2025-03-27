using AutoMapper;
using DigitalWallet.DTO;
using DigitalWallet.Interface;
using DigitalWallet.Models;
using DigitalWallet.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalWallet.Controllers
{
    [Authorize]
    [ApiController]
    [Route("transaction")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public TransactionController(ITransactionRepository transactionRepository, IMapper mapper, IUserRepository userRepository)
        {
            _transactionRepository = transactionRepository ?? throw new Exception("Repository Not Found!");
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> Transfer([FromBody] TransactionDto input)
        {
            try
            {
                var transaction = _mapper.Map<Transaction>(input);

                var sender = await _userRepository.GetById(transaction.UserId);

                var receiver = new User();
                if (!string.IsNullOrEmpty(transaction.WalletNumberDestination))
                    receiver = await _userRepository.GetUserByWalletNumberAsync(transaction.WalletNumberDestination) ?? null;

                if (sender == null)
                    return BadRequest("Usuário remetente não encontrado.");

                if (sender.Balance < transaction.Value)
                    return BadRequest("Saldo insuficiente para a transferência.");

                sender.Balance -= transaction.Value;

                if(receiver != null)
                    receiver.Balance += transaction.Value;

                _userRepository.Update(sender);

                _transactionRepository.Create(transaction);

                return Ok("Transação efetuada com sucesso !");

            }
            catch (Exception e)
            {
                return BadRequest("Erro ao realizar transferência.");
            }
        }

        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit([FromBody] TransactionDto input)
        {
            try
            {
                var transaction = _mapper.Map<Transaction>(input);

                var sender = await _userRepository.GetById(transaction.UserId);
                
                if (sender == null)
                    return BadRequest("Usuário remetente não encontrado.");

                sender.Balance += transaction.Value;
                transaction.CPFDestination = sender.CPF;

                _userRepository.Update(sender);
                _transactionRepository.Create(transaction);

                return Ok("Transação efetuada com sucesso !");

            }
            catch (Exception e)
            {
                return BadRequest("Erro ao realizar transferência.");
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetTransactions(int userId)
        {
            var transactions = await _transactionRepository.GetByUser(userId);

            if (!transactions.Any())
                return NotFound("Nenhuma transação encontrada para este usuário.");

            return Ok(transactions);
        }

        [HttpGet("balance/{userId}")]
        public async Task<UserDto> CheckBalance(int userId)
        {
            var user = await _userRepository.GetById(userId);

            if (user == null)
                throw new ArgumentException("Usuário não encontrado.");

            return new UserDto
            {
                Name = user.Name,
                Email = user.Email,
                Balance = user.Balance
            };
        }
    }
}
