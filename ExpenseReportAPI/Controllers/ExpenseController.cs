using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Shared_Layer.DTO;
using Shared_Layer.Interfaces;

namespace ExpenseReportAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpenseController : Controller
    {
        private readonly IExpenseBL _repo;
        public ExpenseController(IExpenseBL repo)
        {
            _repo = repo;
        }
    
        [HttpGet("{userId}/{groupId}")]
        public async Task<IActionResult> GetAllExpenses([FromRoute] Guid userId, [FromRoute] Guid groupId)
        {
            try
            {
                var expense = await _repo.GetExpenseByGroupuserId(userId, groupId);
                Log.Information("Got all the related Expenses");
                return Ok(expense);
            }
            catch(Exception ex)
            {
                Log.Error(ex.Message);
                return BadRequest();
            }
        }
        [Authorize]
        [HttpGet("PendingExpense/{id}")]
        public async Task<IActionResult> PendingExpense([FromRoute] Guid id)
        {
            try
            {
                var expense = await _repo.GetPendingExpense(id);
                Log.Information("Created Expenses Retrieved");
                return Ok(expense);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return BadRequest();
            }
        }
        //[HttpGet]
        //public async Task<IActionResult> GetAllExpense()
        //{
        //    try
        //    {
        //        var expense = await _repo.GetExpenses();
        //        if (expense == null)
        //        {
        //            Log.Warning("No expenses found. Returning NotFound.");
        //            return NotFound("List is empty ");
        //        }
        //        Log.Information("All Expenses {@Expenses}",expense);
        //        return Ok(expense);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(ex.Message);
        //        return NotFound();
        //    }



        //}
        //[HttpGet("GetExpenseGroupId/{id}")]
        //public async Task<IActionResult> GetExpenseByGroupId([FromRoute]Guid id)
        //{
        //    try
        //    {
        //        var expense = await _repo.GetExpenseByGroupId(id);
        //        Log.Information("Expenses Retrieved by ID;{Id}", id);
        //        return Ok(expense);
        //    }
        //    catch(Exception ex) 
        //    {Log.Error(ex.Message);
        //    return NotFound();}
        //}
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult>GetExpenseById([FromRoute]Guid Id)
        {
            try
            {
                var expense = await _repo.GetExpenseById(Id);
                Log.Information("Expnse Group Retrieved by ID:{Id}",Id);
                return Ok(expense);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return NotFound();
            }
        }
        //[HttpGet("GetExpenseByUserId/{id}")]
        //public async Task<IActionResult> GetExpenseByUserId([FromRoute]Guid id)
        //{
        //    try
        //    {
        //        var expense = await _repo.GetExpenseByUserId(id);
        //        Log.Information("Expense Retrieved for User:{UserId}}", id);
        //        return Ok(expense);
        //    }
        //    catch(Exception ex)
        //    {
        //        Log.Error(ex.Message);
        //        return NotFound();
        //    }
        //}
        [Authorize]
        [HttpGet("GetMembersbyId/{expenseId}")]
        public async Task<IActionResult> GetMembersById([FromRoute] Guid expenseId)
        {
            try
            {
                var users = await _repo.GetUsersByExpenseId(expenseId);
                Log.Information("Members Received");
                return Ok(users);
            }
            catch(Exception e)
            {
                Log.Error(e.Message);
                return NotFound();
            }
        }

        //[HttpPost("{userId}/{groupId}")]
        //public async Task<IActionResult> AddExpense([FromRoute] Guid userId, [FromRoute] Guid groupId, [FromBody] IncomingExpense expense)
        //{
        //    try
        //    {
        //        var newExpense = await _repo.AddExpense(userId, groupId, expense);
        //        Log.Information("Expense Added :{@Expenses}", expense);
        //        return Ok(newExpense);

        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(ex.Message);
        //        return BadRequest();
        //    }
        //}
        [Authorize]
        [HttpPost("{userId}/{groupId}")]
        public async Task<IActionResult> AddExpense([FromRoute] Guid userId, [FromRoute] Guid groupId, [FromBody] IncomingExpense expense)
        {
            try
            {
                var newExpense = await _repo.AddExpenseWithMembers(userId, groupId, expense);
                Log.Information("Expense Added :{@Expenses}", expense);
                return Ok(newExpense);

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return BadRequest();
            }
        }
        [Authorize]
        [HttpDelete("Settle/{id}")]
        public async Task<IActionResult> SettleShare(Guid id)
        {
            try
            {
                var expense = await _repo.SettleExpense(id);
                Log.Information("Expense Settled");
                return Ok(expense);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(Guid id)
        {
            try
            {
                var respone = await _repo.DeleteExpense(id);
                Log.Information("Expense Deleted with id :{Id}",id);
                return Ok(respone);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return BadRequest();
            }
        }

        //[HttpPost("AddMembers/{Id}")]
        //public async Task<IActionResult> AddMembers([FromRoute] Guid Id, [FromBody] List<Guid> userId)
        //{
        //    try
        //    {
        //        var members = await _repo.AddMembersToExpense(Id, userId);
        //        Log.Information("Members Added");
        //        return Ok(members);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Error(ex.Message);
        //        return NotFound();
               
        //    }

        //}
    }
}
