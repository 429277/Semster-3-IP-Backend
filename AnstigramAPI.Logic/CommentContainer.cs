using AnstigramAPI.Logic.Interfaces;
using AnstigramAPI.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnstigramAPI.Logic
{
    public class CommentContainer
    {
        private readonly ICommentDAL _commentDAL;
        public CommentContainer(ICommentDAL commentDAL)
        {
            _commentDAL = commentDAL;
        }
        public Comment GetComment(int id)
        {
            Comment comment = _commentDAL.GetComment(id);
            return comment;
        }
        public List<Comment> GetComments()
        {
            List<Comment> comments = _commentDAL.GetComments();
            return comments;
        }
    }
}
