using DigitalWallet.Models;

namespace DigitalWallet.Interface
{
    public interface IUserRepository
    {
        List<User> GetAll();
        Task<User> GetById(int id);
        Task<User> GetUserByEmailAsync(string email);
        void Create(User user);
        void Update(User user);
        Task<User> GetUserByWalletNumberAsync(string walletNumber);
    }
}
