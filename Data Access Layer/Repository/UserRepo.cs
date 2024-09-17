using Data_Access_Layer.Context;
using Data_Access_Layer.Hasher;
using Microsoft.EntityFrameworkCore;
using Shared_Layer.Interfaces;
using Shared_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repository
{
   public class UserRepo : IUserRepo
    {
        private readonly ExpenseDbContext _context;
        public UserRepo(ExpenseDbContext context)
        {
            _context = context;
        }

        public async Task<Users> AddUser(Users user)
        {
            user.Password = PasswordHasher.HashPassword(user.Password);
            user.IsAdmin = false;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return false;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Users>> GetAllUsers()
        {
            var allUser = await _context.Users.ToListAsync();
            return allUser;
        }

        public async Task<Users> GetUserById(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            return user;
        }

        public Users Login(Login login)
        {
            var userAvailable = _context.Users.FirstOrDefault(x => x.Email == login.Email);
            if (userAvailable == null)
            {
                return null;
            }


            return userAvailable;
        }

        public async Task<Users> UpdateUser(Guid id, Users user)
        {
           
            var existingUser = await _context.Users.FindAsync(id);

            if (existingUser == null)
            {
                return null;
            }
            user.Password = PasswordHasher.HashPassword(user.Password);
            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;
            existingUser.IsAdmin = false;

            await _context.SaveChangesAsync();

            return existingUser;
        }
    }
}
