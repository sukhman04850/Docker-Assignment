using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Shared_Layer.DTO;
using Shared_Layer.Interfaces;
using Shared_Layer.Models;
using System.Runtime.CompilerServices;

namespace ExpenseReportAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseGroupController : Controller
    {
        private readonly IExpenseGroupBL _repo;
        public ExpenseGroupController(IExpenseGroupBL repo)
        {
            _repo = repo;
        }
        //[HttpGet("GetAllExpenseGroups")]
        //public async Task<IActionResult> GetAllExpenseGroups()
        //{
        //    try
        //    {
        //        var expenseGroups = await _repo.GetAllExpenseGroups();
        //        Log.Information("Expense Groups Retrieved");
        //        return Ok(expenseGroups);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(ex.Message);
        //        return NotFound();
        //    }
        //}
        [Authorize]
        [HttpGet("GetMembers/{id}")]
        public async Task<IActionResult>GetMembers(Guid id)
        {
            try
            {
                var expense = await _repo.GetUsersByGroupId(id);
                Log.Information("All Members received");
                return Ok(expense);
            }
            catch(Exception ex)
            {
                Log.Error(ex.Message);
                return NotFound(ex);
            }
        }
        [Authorize]
        [HttpGet("GetExpenseGroupById/{id}")]
        public async Task<IActionResult> GetExpenseGroupById([FromRoute] Guid id)
        {
            try
            {
                var expenseGroup = await _repo.GetExpenseGroupById(id);
                Log.Information("Expense Group Retrieved");
                return Ok(expenseGroup);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return NotFound();
            }
        }
        [Authorize]
        [HttpPost("CreateExpenseGroup/{id}")]
        public async Task<IActionResult> CreateExpenseGroup([FromRoute] Guid id,[FromBody] IncomingExpenseGroupDTO expenseGroup)
        {
            try
            {
                var groupList = await _repo.CreateExpenseGroup(id,expenseGroup);
                Log.Information("Expense Group Created");
                return Ok(groupList);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return NotFound();
            }
        }
        //[HttpPut("UpdateExpenseGroup/{id}")]
        //public async Task<IActionResult> UpdateExpenseGroup([FromRoute] Guid id, [FromBody] ExpenseGroup expenseGroup)
        //{
        //    try
        //    {
        //        var groupList = await _repo.UpdateExpenseGroup(id, expenseGroup);
        //        Log.Information("Expense Group Updated");
        //        return Ok(groupList);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(ex.Message);
        //        return NotFound();
        ////    }
        //}
        //[HttpDelete("DeleteExpenseGroup/{id}")]
        //public async Task<IActionResult> DeleteExpenseGroup([FromRoute] Guid id)
        //{
        //    try
        //    {
        //        var groupList = await _repo.DeleteExpenseGroup(id);
        //        Log.Information("Expense Group Deleted");
        //        return Ok(groupList);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(ex.Message);
        //        return NotFound();
        //    }
        //}
        [Authorize]
        [HttpGet("GetExpenseGroupByUserId/{userId}")]
        public async Task<IActionResult> GetExpenseGroupByUserId([FromRoute] Guid userId)
        {
            try
            {
                var groupList = await _repo.GetExpenseGroupByUserId(userId);
                Log.Information("Expense Group Retrieved");
                return Ok(groupList);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return NotFound();
            }
        }
        [Authorize]
        [HttpPost("AddMembers/{GroupId}")]
        public async Task<IActionResult> AddMembers([FromRoute] Guid GroupId, [FromBody] List<Guid> userId)
        {
            try
            {
                var members = await _repo.AddMembers(GroupId, userId);
                Log.Information("Members Added");
                return Ok(members);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return NotFound();
            }
        }
        [Authorize]
        [HttpGet("GetNonMembers/{groupId}")]
        public async Task<IActionResult> GetNonMembers(Guid groupId)
        {
            try
            {
                var nonmember = await _repo.GetNonMemberByGroupId(groupId);
                Log.Information("All Non Members received");
                return Ok(nonmember);
            }
            catch(Exception ex)
            {
                Log.Error(ex.Message);
                return NotFound();
            }
        }
    }
}
