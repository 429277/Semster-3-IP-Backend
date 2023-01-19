using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AnstigramAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : Controller
    {

        public CommentController()
        {

        }

        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {

            return Json("");
        }

        [HttpGet]
        public JsonResult Get()
        {

            return Json("");
        }
    }
}
