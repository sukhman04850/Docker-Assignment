using Shared_Layer.DTO;
using Shared_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Layer.Interfaces
{
    public interface IExpenseRepo
    {
        Task <List<Expenses>>GetExpenses();
        Task<Expenses> GetExpenseById(Guid Id);
        Task<Expenses> AddExpenseWithMembers(Guid userId, Guid groupId, Expenses expense, List<Guid> userIds);
        Task<Expenses> UpdateExpense(Guid id,Expenses expenses);
        Task<bool> DeleteExpense(Guid id);
      
        Task<List<Expenses>> GetExpenseByGroupuserId(Guid userId, Guid groupId);
      
        Task<List<Expenses>> GetCreatedExpense(Guid userId);
        Task<List<ExpenseShareDTO>>GetPendingExpense(Guid userId);
   
        Task<List<Users>> GetUsersByExpenseId(Guid groupId);
        Task<bool> SettleExpense(Guid id);

    }
}
