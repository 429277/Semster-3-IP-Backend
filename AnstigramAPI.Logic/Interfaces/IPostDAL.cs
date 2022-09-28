using AnstigramAPI.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnstigramAPI.Logic.Interfaces
{
    public interface IPostDAL
    {
        public List<Post> GetPosts();
    }
}
