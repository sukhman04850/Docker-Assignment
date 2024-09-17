using Data_Access_Layer.Context;
using Microsoft.EntityFrameworkCore;
using Shared_Layer.DTO;
using Shared_Layer.Interfaces;
using Shared_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Repository
{
    public class ExpensesRepo : IExpenseRepo
    {
        private readonly ExpenseDbContext _context;
        public ExpensesRepo(ExpenseDbContext context)
        {
            _context = context;
        }
        public async Task<Expenses> AddExpenseWithMembers(Guid userId, Guid groupId, Expenses expense, List<Guid> userIds)
        {

            expense.UserId = userId;
            expense.ExpenseGroupId = groupId;


            var newExpense = _context.Expenses.Add(expense);


            var users = await _context.Users.Where(u => userIds.Contains(u.Id)).ToListAsync();
            if (users == null || users.Count == 0)
            {
                throw new Exception("Users not found");
            }


            decimal shareAmount = expense.Amount / (userIds.Count + 1);

            foreach (var user in users)
            {
                var expenseShare = new ExpenseShare
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    ExpenseId = expense.Id,
                    Amount = shareAmount
                };
                _context.ExpenseShare.Add(expenseShare);
            }


            //var creatorExpenseShare = new ExpenseShare
            //{
            //    Id = Guid.NewGuid(),
            //    UserId = userId,
            //    ExpenseId = expense.Id,
            //    Amount = 0
            //};
            //_context.ExpenseShare.Add(creatorExpenseShare);

            await _context.SaveChangesAsync();

            return expense;
        }



        public async Task<bool> DeleteExpense(Guid id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
            {
                return false;
            }
            _context.Expenses.Remove(expense);
            _context.SaveChanges();
            return true;
        }

      

        public async Task<Expenses> GetExpenseById(Guid Id)
        {
            var expense = await _context.Expenses.FindAsync(Id);
            if (expense == null)
            {
                throw new Exception("Expense Not Found");
            }
            return expense;
        }



        public async Task<List<Expenses>> GetExpenses()
        {
            var allExpenses = await _context.Expenses.ToListAsync();

            return allExpenses;
        }

        public async Task<Expenses> UpdateExpense(Guid id, Expenses expenses)
        {
            var existingExpense = await _context.Expenses.FindAsync(id);
            if (existingExpense == null)
            {
                throw new Exception("Expense Not Found");
            }
            _context.Entry(existingExpense).CurrentValues.SetValues(expenses);
            await _context.SaveChangesAsync();
            return expenses;

        }

       

        public async Task<List<Users>> GetUsersByExpenseId(Guid expenseId)
        {
            var expenseUserIds = await _context.Expenses
            .Where(e => e.Id == expenseId)
            .Select(e => e.UserId)
            .ToListAsync();

            var expenseShareUserIds = await _context.ExpenseShare
                .Where(es => es.ExpenseId == expenseId)
                .Select(es => es.UserId)
                .ToListAsync();
            var allUserIds = expenseUserIds.Concat(expenseShareUserIds).Distinct().ToList(); var users = await _context.Users
            .Where(u => allUserIds.Contains(u.Id))
            .ToListAsync();

            return users;
        }

        public async Task<List<Expenses>> GetCreatedExpense(Guid userId)
        {
            var expense = await _context.Expenses.Where(x => x.UserId == userId).ToListAsync();
            return expense;
        }

        public async Task<List<ExpenseShareDTO>> GetPendingExpense(Guid userId)
        {
            var expenseShares = await _context.ExpenseShare
                                        .Where(x => x.UserId == userId)
                                        .Include(x => x.Expense)
                                        .ToListAsync();


            return expenseShares.Select(es => new ExpenseShareDTO
            {
                Id = es.Id,
                UserId = es.UserId,
                Amount = es.Amount,
                ExpenseId = es.ExpenseId,
                Expense = es.Expense
            }).ToList();
        }

        public async Task<List<Expenses>> GetExpenseByGroupuserId(Guid userId, Guid groupId)
        {

            var expenseGroup = await _context.ExpenseGroup
                                             .Include(eg => eg.Expenses)
                                             .ThenInclude(e => e.ExpenseShares)
                                             .FirstOrDefaultAsync(eg => eg.GroupId == groupId);
            if (expenseGroup == null)
            {
                return new List<Expenses>();
            }

            var filteredExpenses = expenseGroup.Expenses
                                               .Where(e => e.UserId == userId || e.ExpenseShares.Any(es => es.UserId == userId))
                                               .ToList();
            return filteredExpenses;
        }

        public async Task<bool> SettleExpense(Guid id)
        {
            var expenseShare = await _context.ExpenseShare.FirstOrDefaultAsync(x => x.Id == id);

            if (expenseShare != null)
            {
                _context.ExpenseShare.Remove(expenseShare);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
