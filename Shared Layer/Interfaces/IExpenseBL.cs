using Shared_Layer.DTO;
using Shared_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Layer.Interfaces
{
    public interface IExpenseBL
    {
        Task<List<Expenses>> GetExpenses();
        Task<Expenses> GetExpenseById(Guid Id);
        Task<Expenses> UpdateExpense(Guid id,IncomingExpense expenses);
        Task<bool> DeleteExpense(Guid id);
        Task<Expenses> AddExpenseWithMembers(Guid userId, Guid groupId, IncomingExpense expense);
        Task<List<Expenses>> GetExpenseByGroupuserId(Guid userId, Guid groupId);
        
       
     
        Task<List<ExpenseShareDTO>> GetPendingExpense(Guid userId);
        Task<bool> SettleExpense(Guid id);
        Task<List<Users>> GetUsersByExpenseId(Guid expenseId);

    }
}
