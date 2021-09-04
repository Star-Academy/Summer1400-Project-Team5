using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Talent.Data.Entities;
using Talent.Logic;
using Talent.Models;

namespace Talent.Controllers
{
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly TokenGenerator _tokenGenerator;

        public AccountController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager, TokenGenerator tokenGenerator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenGenerator = tokenGenerator;
        }

        [HttpPost]
        [Route("{controller}/{action}")]
        public async Task<IActionResult> Register([FromBody] UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser() { Email = userModel.Email };
                var result = await _userManager.CreateAsync(user, userModel.Password);
                if (result.Succeeded)
                {
                    return Ok(new { Token = _tokenGenerator.CreateToken(user) });
                }
                else
                {
                    return BadRequest(new { Error = result.Errors });
                }
            }
            return BadRequest(new { Error = "value cannot be null"});
        }

        [HttpPost]
        [Route("{controller}/{action}")]
        public async Task<IActionResult> Login([FromBody] UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(userModel.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, userModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        return Ok(new { Token = _tokenGenerator.CreateToken(user) });
                    }
                }
                return BadRequest(new { Error = "username or password invalid" });
            }
            return BadRequest(new { Error = "value cannot be null" });
        }

        [HttpPost]
        [Authorize]
        [Route("{controller}/{action}")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }
}