using AnstigramAPI.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnstigramAPI.Logic.Interfaces
{
    public interface IUserDAL
    {
        public User GetUser(int id);
        public List<User> GetUsers();
    }
}
