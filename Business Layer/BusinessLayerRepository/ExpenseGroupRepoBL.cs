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
    public class ExpenseGroupRepoBL : IExpenseGroupBL
    {
        private readonly IExpenseGroupRepo _repo;
        private readonly IMapper _mapper;
        public ExpenseGroupRepoBL(IExpenseGroupRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<Users>> AddMembers(Guid GroupId, List<Guid> userId)
        {
            var members = await _repo.AddMembers(GroupId, userId);
            return members;
        }

        public async Task<ExpenseGroup> CreateExpenseGroup(Guid Id, IncomingExpenseGroupDTO expenseGroup)
        {
            var group = _mapper.Map<ExpenseGroup>(expenseGroup);
            var groupList = await _repo.CreateExpenseGroup(Id,group);
           
            return groupList;
        }

        public async Task<bool> DeleteExpenseGroup(Guid id)
        {
            return await _repo.DeleteExpenseGroup(id);
        }

        public async Task<List<ExpenseGroup>> GetAllExpenseGroups()
        {
            return await _repo.GetAllExpenseGroups();
        }

        public async Task<ExpenseGroup> GetExpenseGroupById(Guid id)
        {
            return await _repo.GetExpenseGroupById(id);
        }

        public async Task<List<ExpenseGroupDTO>> GetExpenseGroupByUserId(Guid userId)
        {
            var expenseGroups = await _repo.GetExpenseGroupByUserId(userId);
            var expenseGroupDTOs = _mapper.Map<List<ExpenseGroupDTO>>(expenseGroups);
            return expenseGroupDTOs;
        }

        public async Task<List<Users>> GetNonMemberByGroupId(Guid groupId)
        {
            var nonmembers = await _repo.GetNonMemberByGroupId(groupId);
            return nonmembers;
        }

        public async Task<List<Users>> GetUsersByGroupId(Guid groupId)
        {
            var expense = await _repo.GetUsersByGroupId(groupId);
            return expense;
        }

        public async Task<ExpenseGroup> UpdateExpenseGroup(Guid id, ExpenseGroup expenseGroup)
        {
            return await _repo.UpdateExpenseGroup(id, expenseGroup);
        }
    }
}
