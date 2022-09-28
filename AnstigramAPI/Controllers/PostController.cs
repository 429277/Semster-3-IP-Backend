using AnstigramAPI.Logic.Interfaces;
using AnstigramAPI.Logic.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AnstigramAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : Controller
    {
        private readonly IPostDAL _postDAL;
        public PostController(IPostDAL postDAL)
        {
            _postDAL = postDAL;
        }

        [HttpGet]
        public JsonResult Get()
        {
            List<Post> posts = _postDAL.GetPosts();
            return new JsonResult(posts);
        }
    }
}
