using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Project.Models;
using Project.Services;

namespace Project.Controllers
{
    [Produces("application/json")]
    [Route("api/Login")]
    public class LoginController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebService _webService;

        public LoginController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager, IWebService webService)
        {
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userManager = userManager;
            _webService = webService;
        }


        [HttpGet]
        [Route("ExternalLogin")]
        [AllowAnonymous]
        public async Task<IActionResult> ValidateAndGenerateToken(string googleToken)
        {
            if (string.IsNullOrEmpty(googleToken))
            {
                return new EmptyResult();
            }
            var info = await _webService.ValidateGoogleToken(googleToken);
            
            if (info == null)
            {
                return RedirectToAction(nameof(LoginFail));
            }

            var email = info.email;

            //Register or login user here
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                await CreateRoles();
                var result = await _userManager.CreateAsync(new IdentityUser
                {
                    Email = email,
                    UserName = email
                });
                if (result.Succeeded)
                {
                    user = await _userManager.FindByEmailAsync(email);
                    await _userManager.AddToRoleAsync(user, "User");
                }
            }

            //Because we're not saving users, just generate and send JWT token
            var token = await GenerateToken(email);
            var authToken = await GetAuthorizationToken(user, token);
            return Ok(authToken);
        }

        private async Task CreateRoles()
        {
            if (!await _roleManager.RoleExistsAsync("User"))
            {
                var role = new IdentityRole {Name = "User"};
                await _roleManager.CreateAsync(role);
            }

            if (!await _roleManager.RoleExistsAsync("Administrator"))
            {
                var role = new IdentityRole {Name = "Administrator"};
                await _roleManager.CreateAsync(role);
            }
        }

        public async Task<dynamic> GetAuthorizationToken(IdentityUser user, string token)
        {
            var requestAt = DateTime.Now;
            var expiresIn = requestAt + TimeSpan.FromMinutes(120);

            return new {
                requestAt,
                expiresIn,
                tokenType = AuthenticationOptions.TokenType,
                token,    
                isAdmin = await _userManager.IsInRoleAsync(user, "Administrator")
            };
        }
        
        private async Task<string> GenerateToken(string email)
        {
            var identity = new ClaimsIdentity(
                new GenericIdentity(email, "TokenAuth"));
            var user = await _userManager.FindByEmailAsync(email);
            foreach (var role in await _userManager.GetRolesAsync(user))
            {
                identity.AddClaim(new Claim("role", role));
            }
            var expiresIn = DateTime.UtcNow + TimeSpan.FromMinutes(120);
            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = AuthenticationOptions.Issuer,
                Audience = AuthenticationOptions.Audience,
                SigningCredentials = AuthenticationOptions.SigningCredentials,
                Subject = identity,
                Expires = expiresIn
            });

            return handler.WriteToken(securityToken);
        }

        [HttpGet]
        public ActionResult LoginFail()
        {
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }
    }
}


//        [HttpDelete("SignOut")]
//        [Authorize(Policy = "Bearer")]
//        public async Task<IActionResult> SignOut()
//        {
//            var info = await 
//            if (info == null)
//            {
//                return RedirectToAction(nameof(ExternalLogin));
//            }
//            
//            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
//            if (user == null)
//                return new StatusCodeResult(StatusCodes.Status404NotFound);
//
//            var shit = info.AuthenticationTokens.ToString();
//            
////            _userManager.RemoveAuthenticationTokenAsync(user, info.LoginProvider, )
//            return Ok();
//        }