using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.IdentityModel.Tokens;
using Project.Models;

namespace Project.Controllers
{
    [Produces("application/json")]
    [Route("api/Login")]
    public class LoginController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public LoginController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // LogIn method
        [Route("External")]
        [HttpGet]
        public ActionResult ExternalLogin(string provider = "Google")
        {
            // Request a redirect to the external login provider.
            const string redirectUrl = "/api/Login/ExternalCallback";
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        [HttpGet]
        [Route("ExternalCallback")]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                return BadRequest();
            }
            
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(LoginFail));
            }

            var email = info.Principal.FindFirstValue(ClaimTypes.Email);

            //Register or login user here
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                await _userManager.CreateAsync(new IdentityUser
                {
                    Email = email,
                    UserName = email
                });
            }

            //Because we're not saving users, just generate and send JWT token
            var token = GenerateToken(email);
            return Ok(token);
        }

        private string GenerateToken(string email)
        {
            var identity = new ClaimsIdentity(
                new GenericIdentity(email, "TokenAuth"));
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