using Shared_Layer.DTO;
using Shared_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Layer.Interfaces
{
    public interface IUserRepoBL
    {
        Task<Users> GetUserById(Guid id);
        Users Login(Login login);
        Task<Users> AddUser(IncomingUser user);
        Task<Users> UpdateUser(Guid id, IncomingUser user);
        Task<bool> DeleteUser(Guid id);
        Task<List<Users>> GetAllUsers();
    }
}
