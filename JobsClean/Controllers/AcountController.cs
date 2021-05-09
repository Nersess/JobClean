using AutoMapper;
using Core.Domains.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JobsClean.Web.Controllers
{
    public class AcountController :_BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper mapper;

        public AcountController(IMapper mapper, UserManager<User> userManager, ILogger<AcountController> logger) : base(logger)
        {
            this.mapper = mapper;
            this._userManager = userManager;
        }

        private async Task CookieLogIn(User user)
        {
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, GetUserClaims(user), GetAuthenticationProperties());
        }

        private ClaimsPrincipal GetUserClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Sid, user.Id.ToString())
            };           

            return new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
        }

        private AuthenticationProperties GetAuthenticationProperties()
        {
            var authProperties = new AuthenticationProperties()
            {
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddDays(1),
                AllowRefresh = true
            };

            return authProperties;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] RegisterModel model)
        {
            User user = await _userManager.FindByEmailAsync(model.Email);
            
            if (user == null)
            {
                user = mapper.Map<RegisterModel, User>(model);
                await _userManager.CreateAsync(user, model.Password);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.FirstName),
                    new Claim(ClaimTypes.Surname,user.LastName)
                };
                await _userManager.AddClaimsAsync(user, claims);

                await CookieLogIn(user);
                return Ok();
            }
            return BadRequest("Please Input new credentials.");
        }

        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);

            if (user != null)
            {
                if (await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    await CookieLogIn(user);
                    return Ok();
                }
                else
                {
                    return BadRequest("Incorrect credentials.");
                }
            }
            return BadRequest("Incorrect credentials.");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Signout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok();
        }
    }
}
