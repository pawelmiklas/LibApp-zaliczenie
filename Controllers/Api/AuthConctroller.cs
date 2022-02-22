using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signIn;

        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signIn)
        {
            _userManager = userManager;
            _signIn = signIn;
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetUserRole()
        {
            _ = await _userManager.AddToRoleAsync(_signIn.UserManager.Users.First(x => x.UserName == _signIn.Context.User.Identity.Name), "User");
            
            return Ok("Please relogin");
        }

        [HttpGet("owner")]
        public async Task<IActionResult> GetOwnerRole()
        {
            _ = await _userManager.AddToRoleAsync(_signIn.UserManager.Users.First(x => x.UserName == _signIn.Context.User.Identity.Name), "Owner");
            
            return Ok("Please relogin");
        }

        [HttpGet("manager")]
        public async Task<IActionResult> GetManagerRole()
        {
            _ = await _userManager.AddToRoleAsync(_signIn.UserManager.Users.First(x => x.UserName == _signIn.Context.User.Identity.Name), "StoreManager");

            return Ok("Please relogin");
        }
    }
}