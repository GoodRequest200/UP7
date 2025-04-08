using Avalonia.Styling;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UP7
{
    public class UserService
    {
        private AppDbContext _context;

        public UserService(AppDbContext context) => _context = context;

        public async Task<List<User>> GetUsersAsync() => await _context.Users.AsNoTracking().ToListAsync();

        public async Task<User> GetUSerByIdAsync(int id) => await _context.Users.FindAsync(id);

        public async Task<int> CreateUserAsync(string login, string password) 
        {
            User user = 
                new User {Id = RandomInteger.GenerateRandomPositiveInt(),Login = login, Password = password };

            await _context.Users.AddAsync(user);

            return user.Id;
        }

        public async void ChangeUser(int id, string login, string password) 
        {
            User selectedUser = await _context.Users.FindAsync(id);

            selectedUser.Login = login;
            selectedUser.Password = password;

            await _context.SaveChangesAsync();
        }

        public async void DeleteUser(int id) 
        {
            User selectedUser = await _context.Users.FindAsync(id);

            _context.Users.Remove(selectedUser);

            await _context.SaveChangesAsync();
        }
    }
}
