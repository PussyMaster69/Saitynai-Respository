using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Models;

namespace Project.Controllers
{
    [Produces("application/json")]
    [Route("api/Login")]
    public class LoginController : Controller
    {
        // LogIn method
        [HttpPost]
        public ActionResult LogIn([FromBody] Login loginData)
        {
            // TODO login sequence 
            return new StatusCodeResult(StatusCodes.Status201Created);
        }

        // LogOut method
        [HttpDelete]
        public ActionResult LogOut()
        {
            return new StatusCodeResult(StatusCodes.Status200OK);
        }
    }
}