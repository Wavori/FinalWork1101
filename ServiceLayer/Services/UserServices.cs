using Microsoft.EntityFrameworkCore;
using ServiceLayer.Data;
using ServiceLayer.Models;

namespace ServiceLayer.Services
{
    public class UsersService
    {
        private readonly ShopContext _context = new();

        public async Task<bool> IsUserExist(string login, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == login && u.Password == password);
            if (user != null)
                return true;

            return false;
        }

        public async Task<User?> GetUserNameByLogin(string login)
            => await _context.Users.FirstOrDefaultAsync(u => u.Login == login);
    }
}
