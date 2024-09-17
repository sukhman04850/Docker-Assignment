using Data_Access_Layer.Context;
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
    public class ExpenseGroupRepo : IExpenseGroupRepo
    {
        private readonly ExpenseDbContext _context;
        private readonly IUserRepo _userRepo;
        public ExpenseGroupRepo(ExpenseDbContext context, IUserRepo userRepo)
        {
            _context = context;
            _userRepo = userRepo;
        }

        public async Task<List<Users>> AddMembers(Guid groupId, List<Guid> userIds)
        {
            
            var group = await _context.ExpenseGroup
                                      .Include(g => g.Users)
                                      .FirstOrDefaultAsync(g => g.GroupId == groupId);

            if (group == null)
            {
                throw new Exception("Group not found");
            }


            var users = await _context.Users
                               .Where(u => userIds.Contains(u.Id) && !u.IsAdmin)
                               .ToListAsync();


            if (users.Count != userIds.Count)
            {
                throw new Exception("One or more users not found");
            }

            
            foreach (var user in users)
            {
                if (!group.Users.Any(u => u.Id == user.Id))
                {
                    group.Users.Add(user);
                }
            }

            
            await _context.SaveChangesAsync();

            return users;
        }


        public async Task<ExpenseGroup> CreateExpenseGroup(Guid id, ExpenseGroup expenseGroup)
        {
            
            var user = await _userRepo.GetUserById(id);

            if (user == null)
            {
                throw new Exception("User not found");
            }

            
            expenseGroup.Users = new List<Users> { user };

            
            expenseGroup.Expenses = new List<Expenses>();

            
            var result = _context.ExpenseGroup.Add(expenseGroup);

            
            await _context.SaveChangesAsync();

            return expenseGroup;
        }


        public async Task<bool> DeleteExpenseGroup(Guid id)
        {
            var expenseGroup = await _context.ExpenseGroup.FindAsync(id);
            if (expenseGroup == null)
            {
                return false;
            }

            _context.ExpenseGroup.Remove(expenseGroup);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ExpenseGroup>> GetAllExpenseGroups()
        {
            return await _context.ExpenseGroup.ToListAsync();
        }

        public async Task<ExpenseGroup> GetExpenseGroupById(Guid id)
        {
            var expenseGroup = await _context.ExpenseGroup.FindAsync(id);
            if (expenseGroup == null)
            {
                throw new Exception("Expense group not found.");
            }
            return expenseGroup;
        }

        public async Task<List<ExpenseGroup>> GetExpenseGroupByUserId(Guid userId)
        {
            var user = await _context.Users
                .Include(u => u.ExpenseGroups)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null || user.ExpenseGroups == null)
            {
                return new List<ExpenseGroup>();
            }
            else
            {
                return user.ExpenseGroups.ToList();
            }
        }

        public async Task<List<Users>> GetNonMemberByGroupId(Guid groupId)
        {
            var usersInGroup = await _context.ExpenseGroup
           .Where(g => g.GroupId == groupId)
           .SelectMany(g => g.Users)
           .Select(u => u.Id)
           .ToListAsync();


            var usersNotInGroup = await _context.Users
              .Where(u => !usersInGroup.Contains(u.Id) && !u.IsAdmin) 
              .ToListAsync();


            return usersNotInGroup;
        }

        public async Task<List<Users>> GetUsersByGroupId(Guid groupId)
        {
            var group = await _context.ExpenseGroup
             .Include(eg => eg.Users)
             .FirstOrDefaultAsync(eg => eg.GroupId == groupId);

            return group?.Users ?? new List<Users>();
        }

        public async Task<ExpenseGroup> UpdateExpenseGroup(Guid id, ExpenseGroup expenseGroup)
        {
            var existingExpenseGroup = await _context.ExpenseGroup.FindAsync(id);
            if (existingExpenseGroup == null)
            {
                throw new Exception("Expense group not found.");
            }

            _context.Entry(existingExpenseGroup).CurrentValues.SetValues(expenseGroup);
            await _context.SaveChangesAsync();
            return expenseGroup;
        }
    }
}
