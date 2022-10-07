using AnstigramAPI.Logic;
using AnstigramAPI.Logic.Interfaces;
using AnstigramAPI.Logic.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AnstigramAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : Controller
    {
        private readonly CommentContainer _commentContainer;
        public CommentController(ICommentDAL commentDAL)
        {
            _commentContainer = new CommentContainer(commentDAL);
        }

        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            Comment comment = _commentContainer.GetComment(id);
            return Json(comment);
        }

        [HttpGet]
        public JsonResult Get()
        {
            List<Comment> comments = _commentContainer.GetComments();
            return Json(comments);
        }
    }
}
