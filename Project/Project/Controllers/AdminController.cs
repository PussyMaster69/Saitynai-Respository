using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.Models;

namespace Project.Controllers
{
    [Produces("application/json")]
    [Route("api/Admin")]
    [Authorize(Policy = "Bearer", Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly MyDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;


        public AdminController(MyDbContext dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        [HttpGet("Users")]
        public ActionResult GetAllUsers()
        {
            var users = _userManager.GetUsersInRoleAsync("User").Result;

            if (users.Count <= 0)
                return new StatusCodeResult(StatusCodes.Status404NotFound);

            var usersList = AddUsersToUserModel(users);
            return Ok(usersList);
        }

        [HttpGet("Administrators")]
        public ActionResult GetAllAdministrators()
        {
            var admins = _userManager.GetUsersInRoleAsync("Administrator").Result;

            if (admins.Count <= 0)
                return new StatusCodeResult(StatusCodes.Status404NotFound);

            var adminsList = AddUsersToUserModel(admins);
            return Ok(adminsList);
        }

        [HttpPut("{email}/{role}")]
        public ActionResult SetRoleToUser(string email, string role)
        {
            var user = _userManager.FindByEmailAsync(email).Result;
            
            if (user == null)
                return new StatusCodeResult(StatusCodes.Status404NotFound);

            IList<string> roles = null;
            switch (role)
            {
                case "Administrator":
                    roles = _userManager.GetRolesAsync(user).Result;
                    _userManager.RemoveFromRolesAsync(user, roles).Wait();
                    _userManager.AddToRoleAsync(user, "Administrator").Wait();
                    break;
                case "User":
                    roles = _userManager.GetRolesAsync(user).Result;
                    _userManager.RemoveFromRolesAsync(user, roles).Wait();
                    _userManager.AddToRoleAsync(user, "User").Wait();
                    break;
                default:
                    return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
            return Ok();
        }

        private List<User> AddUsersToUserModel(ICollection<IdentityUser> userList)
        {
            List<User> usersList = new List<User> {Capacity = userList.Count};
            usersList.AddRange(userList.Select(user => new User()
            {
                Id = user.Id,
                Email = user.NormalizedEmail,
                UserName = user.NormalizedUserName
            }));
            return usersList;
        }
    }
}