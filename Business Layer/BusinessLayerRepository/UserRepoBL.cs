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
    public class UserRepoBL : IUserRepoBL
    {
        private readonly IUserRepo _repo;
        private readonly IMapper _mapper;
        public UserRepoBL(IUserRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Users> AddUser(IncomingUser user)
        {
            var addUser = _mapper.Map<Users>(user);
            var newUser = await _repo.AddUser(addUser);
            return newUser;
        }

        public async Task<bool> DeleteUser(Guid id)
        {
            return await _repo.DeleteUser(id);
        }

        public async Task<Users> GetUserById(Guid id)
        {
            var user = await _repo.GetUserById(id);
            return user;
        }

        public  Users Login(Login login)
        {
           var user = _repo.Login(login);
            return user;
        }

        public async Task<Users> UpdateUser(Guid id, IncomingUser user)
        {
            var newUser = _mapper.Map<Users>(user);
            var updateUser = await _repo.UpdateUser(id,newUser);
            return updateUser;
        }
        public async Task<List<Users>> GetAllUsers()
        {
            return await _repo.GetAllUsers();
        }
    }
}
