using AnstigramAPI.Logic.Interfaces;
using AnstigramAPI.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnstigramAPI.Logic
{
    public class PostContainer
    {
        private IPostDAL _postDAL;
        public PostContainer(IPostDAL postDAL)
        {
            _postDAL = postDAL;
        }

        public List<Post> GetPost()
        {
            return _postDAL.GetPosts();
        }
    }
}
