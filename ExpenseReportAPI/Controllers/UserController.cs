using Business_Layer.BusinessLayerRepository;
using Data_Access_Layer.Hasher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Shared_Layer.DTO;
using Shared_Layer.Interfaces;
using Shared_Layer.Models;

namespace ExpenseReportAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepoBL _repo;
        private readonly IConfiguration _config;
        public UserController(IConfiguration config, IUserRepoBL repo)
        {
            _repo = repo;
            _config = config;
        }
        [HttpPost("LoginUser")]
        public IActionResult LoginUser([FromBody] Login login)
        {
            try
            {
                var userAvailable = _repo.Login(login);
                if (userAvailable == null)
                {
                    Log.Error("User Not Found");

                    return NotFound(new { message = "User Not Found" });
                }
                if (!PasswordHasher.VerifyPassword(login.Password, userAvailable.Password))
                {
                    return BadRequest(new { message = "Wrong Password Entered" });
                }
                Log.Information("User Logged In");
                var token = new JwtServices(_config).GenerateToken(
                     userAvailable.Id.ToString(),
                     userAvailable.Name,
                      userAvailable.Email,
                     userAvailable.IsAdmin);
                return Ok(new { Token = token, UserId = userAvailable.Id });
            }
            catch (Exception ex)
            {
                Log.Error($"An error occurred: {ex.Message}");
                return NotFound();
            }
        }
        [Authorize]
        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] Guid id)
        {
            try
            {
                var user = await _repo.GetUserById(id);
                if (user == null)
                {
                    Log.Error("User Not Found with Id:{Id}", id);
                    return NotFound();
                }
                Log.Information("User Retrieved:{@User}", user);
                return Ok(user);
            }
            catch (Exception ex)
            {
                Log.Error($"An error occurred: {ex.Message}");
                return NotFound();
            }
        }
        [Authorize]
        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] IncomingUser user)
        {
            try
            {
                var newUser = await _repo.AddUser(user);
                Log.Information("User Added:{@User}",newUser);
                return Ok(newUser);
            }
            catch (Exception ex)
            {
                Log.Error($"An error occurred: {ex.Message}");
                return BadRequest();
            }
        }
        [Authorize]
        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
        {
            try
            {
                var user = await _repo.DeleteUser(id);
                if (user == false)
                {
                    return NotFound();
                }
                Log.Information("User Deleted with Id:{Id}",id);
                return Ok("User Deleted");
            }
            catch (Exception ex)
            {
                Log.Error($"An error occurred: {ex.Message}");
                return BadRequest();
            }
        }
        [Authorize]
        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] Guid id, [FromBody] IncomingUser user)
        {
            try
            {
                var updateUser = await _repo.UpdateUser(id, user);
                if (updateUser == null)
                {
                    return NotFound();
                }
                Log.Information("User Updated with Id:{Id} and {@User}",id,updateUser);
                return Ok(updateUser);
            }
            catch (Exception ex)
            {
                Log.Error($"An error occurred: {ex.Message}");
                return BadRequest();
            }
        }
        [Authorize]
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var allUsers = await _repo.GetAllUsers();
                if (allUsers == null || !allUsers.Any())
                {
                    Log.Warning("No users found.");
                    return NotFound();
                }
                Log.Information("All Users Retrieved {@Users}", allUsers);
                return Ok(allUsers);
            }
            catch (Exception ex)
            {
                Log.Error($"An error occurred: {ex.Message}");
                return NotFound();
            }
        }

    }
}
