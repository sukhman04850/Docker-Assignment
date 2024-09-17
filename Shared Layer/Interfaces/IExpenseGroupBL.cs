using Shared_Layer.DTO;
using Shared_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Layer.Interfaces
{
    public interface IExpenseGroupBL
    {
        Task<ExpenseGroup> GetExpenseGroupById(Guid id);
        Task<List<ExpenseGroup>> GetAllExpenseGroups();
        Task<ExpenseGroup> CreateExpenseGroup(Guid id, IncomingExpenseGroupDTO expenseGroup);
        Task<ExpenseGroup> UpdateExpenseGroup(Guid id, ExpenseGroup expenseGroup);
        Task<List<Users>> AddMembers(Guid GroupId, List<Guid> userId);
        Task<bool> DeleteExpenseGroup(Guid id);
        Task<List<ExpenseGroupDTO>> GetExpenseGroupByUserId(Guid userId);
        Task<List<Users>> GetUsersByGroupId(Guid groupId);
        Task<List<Users>> GetNonMemberByGroupId(Guid groupId);
    }
}
