using Shared_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Layer.Interfaces
{
    public interface IUserRepo
    {
        Task<Users> GetUserById(Guid id);
        Users Login(Login login);
        Task <Users>AddUser(Users user);
        Task <Users>UpdateUser(Guid id, Users user);
        Task <bool> DeleteUser(Guid id);
        Task<List<Users>> GetAllUsers();
    }
}
