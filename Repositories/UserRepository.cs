using DigitalWallet.DbContextConfig;
using DigitalWallet.Interface;
using DigitalWallet.Models;
using Microsoft.EntityFrameworkCore;

namespace DigitalWallet.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Create(User user)
        {
            _context.User.Add(user);
            _context.SaveChanges();
        }

        public void Update(User user)
        {
            _context.Update(user);
            _context.SaveChanges();
        }

        public List<User> GetAll()
        {
            return _context.User.ToList();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var user = await _context.User
                .Where(x => x.Email.Contains(email))
                .FirstOrDefaultAsync();
            return user;
        }
        public async Task<User> GetUserByWalletNumberAsync(string walletNumber)
        {
            var user = await _context.User
                .Where(x => x.Email.Contains(walletNumber))
                .FirstOrDefaultAsync();
            return user;
        }

        public async Task<User> GetById(int id)
        {
            var user = await _context.User.FirstAsync(x => x.Id == id);
            return user;
        }
    }
}
