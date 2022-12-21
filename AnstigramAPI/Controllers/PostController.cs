using AnstigramAPI.Interfaces;
using AnstigramAPI.Models;
using AnstigramAPI.Models.Post;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [HttpPost]
        [Route("CreatePost")]
        public JsonResult CreatePost([FromHeader] string Authorization, [FromForm] IFormFile image, [FromForm] string caption)
        {
            var token = Authorization;
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            List<Claim> claims = jwtSecurityToken.Claims as List<Claim>;
            string authId = claims[1].Value;
            //string authId = "auth0|634800719ae95d74a374b4c0";
            CreatePost post = new CreatePost(caption, authId,image);
            _postRepository.Create(post);
            return new JsonResult(null);
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
            List<ReadPost> posts = (List<ReadPost>)_postRepository.GetFeedOfAccount(userId);
            return new JsonResult(posts);
        }

        [HttpGet]
        [Route("GetMyPosts")]
        public JsonResult GetMyPosts([FromHeader] string Authorization)
        {
            //var token = Authorization;
            //var handler = new JwtSecurityTokenHandler();
            //var jwtSecurityToken = handler.ReadJwtToken(token);
            //List<Claim> claims = jwtSecurityToken.Claims as List<Claim>;
            //string userId = claims[1].Value;
            string authId = "auth0|634800719ae95d74a374b4c0";
            List<ReadPost> posts = (List<ReadPost>)_postRepository.GetMyPosts(authId);
            return new JsonResult(posts);
        }
    }
}
