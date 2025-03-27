using DigitalWallet.Models;

namespace DigitalWallet.Interface
{
    public interface ITransactionRepository
    {
        Task<List<Transaction>> GetByUser(int id);
        void Create(Transaction transaction);
    }
}
