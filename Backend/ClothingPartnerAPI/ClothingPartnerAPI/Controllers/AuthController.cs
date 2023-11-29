using ClothingPartnerAPI.DTO;
using ClothingPartnerAPI.DTO.Base;
using ClothingPartnerAPI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ClothingPartnerAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthController(
         UserManager<ApplicationUser> userManager,
         SignInManager<ApplicationUser> signInManager,
         IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("user-login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(username, password, false, false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(username);
                var token = GenerateJwtToken(user);
                return Ok(new { token });
            }
            else
            {
                return Unauthorized();
            }
        }

        // POST: api/Auth/Register
        [HttpPost]
        [Route("user-register")]
        public async Task<IActionResult> Register(string email, string username, string password)
        {
            var userExists = await _userManager.FindByNameAsync(username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO<ApplicationUser> { Message = "User already exists!" });

            ApplicationUser user = new ApplicationUser()
            {
                Email = email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = username
            };

            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO<ApplicationUser> { Message = "User creation failed! Please check user details and try again." });

            return Ok(new ResponseDTO<ApplicationUser> { Message = "User created successfully!" });
        }

        // PUT : api/Auth/Update
        [HttpPut]
        [Route("user-update")]
        public async Task<IActionResult> Update(string username, string email)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO<ApplicationUser> { Message = "User not found!" });

            user.Email = email;
            user.UserName = username;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO<ApplicationUser> { Message = "User update failed! Please check user details and try again." });

            return Ok(new ResponseDTO<ApplicationUser> { Message = "User updated successfully!" });
        }

        // DELETE : api/Auth/Delete
        [HttpDelete]
        [Route("user-delete")]
        public async Task<IActionResult> Delete(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO<ApplicationUser> { Message = "User not found!" });

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO<ApplicationUser> { Message = "User delete failed! Please check user details and try again." });

            return Ok(new ResponseDTO<ApplicationUser> { Message = "User deleted successfully!" });
        }

        // POST: api/Auth/Logout
        [HttpPost]
        [Route("user-logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new ResponseDTO<ApplicationUser> { Message = "User logged out successfully!" });
        }

        // POST: api/Auth/ChangePassword
        [HttpPost]
        [Route("user-change-password")]
        public async Task<IActionResult> ChangePassword(string username, string oldPassword, string newPassword)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO<ApplicationUser> { Message = "User not found!" });

            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO<ApplicationUser> { Message = "User password change failed! Please check user details and try again." });

            return Ok(new ResponseDTO<ApplicationUser> { Message = "User password changed successfully!" });
        }
        
        // GET: api/Auth/GetUser
        [HttpGet]
        [Route("user-get")]
        public async Task<IActionResult> GetUser(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO<ApplicationUser> { Message = "User not found!" });

            return Ok(new ResponseDTO<ApplicationUser> { Data = user, Message = "User found successfully!" });
        }

        // GET: api/Auth/GetUsers
        [HttpGet]
        [Route("user-get-all")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            if (users == null)
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDTO<List<ApplicationUser>> { Message = "Users not found!" });

            return Ok(new ResponseDTO<List<ApplicationUser>> { Data = users, Message = "Users found successfully!" });
        }

        private string GenerateJwtToken(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id)
        }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

}
