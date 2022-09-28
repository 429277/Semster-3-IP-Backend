using AnstigramAPI.Logic.Interfaces;
using AnstigramAPI.Logic.Models;
using System;
using System.Collections.Generic;

namespace AnstigramAPI.Logic
{
    public class UserContainer
    {
        private readonly IUserDAL _userDAL;
        public UserContainer(IUserDAL userDAL)
        {
            _userDAL = userDAL;
        }

        public User GetUser(int id)
        {
            return _userDAL.GetUser(id);
        }
        public List<User> GetUsers()
        {
            return _userDAL.GetUsers();
        }
    }
}
