using DigitalWallet.DbContextConfig;
using DigitalWallet.Interface;
using DigitalWallet.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalWallet.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;
        public TransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Create(Transaction transaction)
        {
            _context.Transaction.Add(transaction);
            _context.SaveChanges();
        }

        public async Task<List<Transaction>> GetByUser(int id)
        {
            var transaction = _context.Transaction.Where(x => x.UserId == id).ToList();
            return transaction;
        }
    }
}
