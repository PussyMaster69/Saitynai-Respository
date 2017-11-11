using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.IdentityModel.Tokens;

namespace Project.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Login")]
    public class LoginController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public LoginController(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        // LogIn method
        [HttpGet]
        public ActionResult LogIn([FromBody] Login loginData)
        {
            // TODO login sequence 
            return new StatusCodeResult(StatusCodes.Status201Created);
        }
        
        [HttpGet]
        public ActionResult LogIn()
        {
            // TODO login sequence 
            return new StatusCodeResult(StatusCodes.Status200OK);
        }

        // LogIn method
        [Route("External")]
        [HttpGet]
        public ActionResult ExternalLogin(string provider="Google")
        {
            // Request a redirect to the external login provider.
            var redirectUrl = "/api/Login/ExternalCallback";
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
                //Error when validating externally
                return BadRequest();
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                //No info returned
                return RedirectToAction(nameof(Login));
            }

            //Register or login user here

            //Because we're not saving users, just generate and send JWT token
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            var token = GenerateToken(email);
            return Ok(token);
        }

        private string GenerateToken(string email)
        {
            var expiresIn = DateTime.UtcNow + TimeSpan.FromMinutes(120);
            var handler = new JwtSecurityTokenHandler();
            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(email, "TokenAuth")
            );

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

        // LogOut method
        [HttpDelete]
        public ActionResult LogOut()
        {
            return new StatusCodeResult(StatusCodes.Status200OK);
        }
    }
}