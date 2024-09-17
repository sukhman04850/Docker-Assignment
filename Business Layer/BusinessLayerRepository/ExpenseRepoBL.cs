using AutoMapper;
using Shared_Layer.DTO;
using Shared_Layer.Interfaces;
using Shared_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Layer.BusinessLayerRepository
{
    public class ExpenseRepoBL : IExpenseBL
    {
        private readonly IExpenseRepo _repo;
        private readonly IMapper _mapper;
        public ExpenseRepoBL(IExpenseRepo repo,IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Expenses> AddExpenseWithMembers(Guid userId, Guid groupId, IncomingExpense expense)
        {
            var expenses = _mapper.Map<Expenses>(expense);
            var addExpense = await _repo.AddExpenseWithMembers(userId, groupId, expenses,expense.Payers);
            return addExpense;
        }



       

        public async Task<bool> DeleteExpense(Guid id)
        {
            return await _repo.DeleteExpense(id);
        }

        public async Task<List<Expenses>> GetCreatedExpense(Guid userId)
        {
            var expense = await _repo.GetCreatedExpense(userId);
            return expense;
        }


        public async Task<List<Expenses>> GetExpenseByGroupuserId(Guid userId, Guid groupId)
        {
            var expenses = await _repo.GetExpenseByGroupuserId(userId, groupId);
            return expenses;
        }

        public async Task<Expenses> GetExpenseById(Guid Id)
        {
            var expense = await _repo.GetExpenseById(Id);
            return expense;
        }

        public async Task<List<Expenses>> GetExpenses()
        {
           var allExpense = await _repo.GetExpenses();
            return allExpense;
        }

        public async Task<List<ExpenseShareDTO>> GetPendingExpense(Guid userId)
        {
            var expense = await _repo.GetPendingExpense(userId);
            return expense;
        }

        public async Task<List<Users>> GetUsersByExpenseId(Guid expenseId)
        {
            var users = await _repo.GetUsersByExpenseId(expenseId);
            return users;
        }

        public async Task<bool> SettleExpense(Guid id)
        {
            var expense = await _repo.SettleExpense(id);
            return expense;
        }

        public async Task<Expenses> UpdateExpense(Guid id, IncomingExpense expenses)
        {
            var update = _mapper.Map<Expenses > (expenses);
            var newExpense = await _repo.UpdateExpense(id, update);
            return newExpense;
        }
    }
}
