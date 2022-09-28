using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using AnstigramAPI.Logic;
using AnstigramAPI.Logic.Interfaces;
using AnstigramAPI.Logic.Models;
using System.Collections.Generic;

namespace AnstigramAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserContainer _userContainer;

        public UserController(IConfiguration configuration, IUserDAL userDAL)
        {
            _configuration = configuration;
            _userContainer = new UserContainer(userDAL);
        }

        
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            User user = _userContainer.GetUser(id);

            return new JsonResult(user);
        }

        [HttpGet]
        public JsonResult Get()
        {
            List<User> users = _userContainer.GetUsers();

            return new JsonResult(users);
        }
    }
}
