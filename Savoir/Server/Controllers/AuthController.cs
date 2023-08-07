using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Savoir.DataAccess;
using Savoir.Server.Helper;
using Savoir.Server.Resources;

namespace Savoir.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IJwtUtils _jwtUtils;
        public AuthController(ApplicationDbContext context, IJwtUtils jwtUtils)
        {
            _context = context;
            _jwtUtils = jwtUtils;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserModel userModel)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.UserName == userModel.UserName && user.Password == userModel.Password);
            if (user == null)
            {
                return BadRequest("User not found.");
            }
            return Ok(new
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Token = _jwtUtils.GenerateToken(user)
            });
        }
    }
}
