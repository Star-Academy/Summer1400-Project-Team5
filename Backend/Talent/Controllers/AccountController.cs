using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Talent.Data.Entities;
using Talent.Logic;
using Microsoft.AspNetCore.Authorization;

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
        public async Task<IActionResult> Register(string email, string password)
        {
            var user = new AppUser() { Email = email };
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded) 
            {
                return Ok(new {Token = _tokenGenerator.CreateToken(user)});
            }
            else 
            {
                return BadRequest(new { Error = result.Errors});
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
                if (result.Succeeded)
                {
                    return Ok(new {Token = _tokenGenerator.CreateToken(user)});
                }
            }
            return BadRequest(new { Error = "username or password invalid"});
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }
    }
}