using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using AnstigramAPI.Interfaces;
using AnstigramAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System;

namespace AnstigramAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAccountRepository _accountRepository;

        public UserController(IConfiguration configuration, IAccountRepository accountRepository)
        {
            _configuration = configuration;
            _accountRepository = accountRepository;
            
        }
        
        [HttpGet("{id}")]
        public JsonResult Get(string id)
        {
            Account user = _accountRepository.GetUser(id);

            return new JsonResult(user);
        }

        [HttpGet]
        [Route("GetFollowers")]
        public IActionResult GetFollowers([FromHeader] string Authorization)
        {
            var token = Authorization;
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            List<Claim> claims = jwtSecurityToken.Claims as List<Claim>;
            string userId = claims[1].Value;
            IEnumerable<Account> accounts = _accountRepository.GetFollowedAccounts(userId);
            return new JsonResult(accounts);
        }

        [HttpGet]
        [Route("GetRecommends")]
        public IActionResult GetRecommends()
        {
            IEnumerable<Account> accounts = _accountRepository.GetAccountRecommends();
            return new JsonResult(accounts);
        }

        [HttpPost]
        [Route("Follow")]
        public bool Follow([FromHeader] string Authorization, [FromForm] int followUserId)
        {
            var token = Authorization;
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);
            List<Claim> claims = jwtSecurityToken.Claims as List<Claim>;
            string authId = claims[1].Value;
            _accountRepository.FollowAccount(authId, followUserId);
            return true;
        }
    }
}
