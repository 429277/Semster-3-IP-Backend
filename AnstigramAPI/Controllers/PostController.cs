using AnstigramAPI.Interfaces;
using AnstigramAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AnstigramAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;
        public PostController(IPostRepository postRespository)
        {
            _postRepository = postRespository;
        }

        //[HttpGet]
        //public JsonResult Get()
        //{
        //    List<Post> posts = _postRepository.GetPosts();
        //    return new JsonResult(posts);
        //}

        [HttpGet]
        [Route("GetFeed")]
        public JsonResult GetFeed([FromHeader] string Authorization)
        {
            //var token = Authorization;
            //var handler = new JwtSecurityTokenHandler();
            //var jwtSecurityToken = handler.ReadJwtToken(token);
            //List<Claim> claims = jwtSecurityToken.Claims as List<Claim>;
            //string userId = claims[1].Value;
            string userId = "auth0|634800719ae95d74a374b4c0";
            List<Post> posts = (List<Post>)_postRepository.GetFeedOfAccount(userId);
            return new JsonResult(posts);
        }
    }
}
