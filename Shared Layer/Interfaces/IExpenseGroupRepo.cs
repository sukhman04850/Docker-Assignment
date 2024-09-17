using Shared_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Layer.Interfaces
{
    public interface IExpenseGroupRepo
    {
        Task <ExpenseGroup> GetExpenseGroupById(Guid id);
        Task<List<ExpenseGroup>> GetAllExpenseGroups();
        Task<ExpenseGroup> CreateExpenseGroup(Guid id,ExpenseGroup expenseGroup);
        Task<ExpenseGroup> UpdateExpenseGroup(Guid id, ExpenseGroup expenseGroup);
        Task<List<Users>> AddMembers(Guid GroupId, List<Guid> userId);
        Task<bool> DeleteExpenseGroup(Guid id);
        Task<List<ExpenseGroup>> GetExpenseGroupByUserId(Guid userId);
        Task<List<Users>> GetUsersByGroupId(Guid groupId);
        Task<List<Users>> GetNonMemberByGroupId(Guid groupId);
    }
}
