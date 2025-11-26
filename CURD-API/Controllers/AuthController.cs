using CURD_API.DataBase;
using CURD_API.Models;
using CURD_API.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace CURD_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(AuthDto dto)
        {
            if (await _context.AuthUsers.AnyAsync(x => x.Username == dto.Username))
                return BadRequest("User already exists");

            var user = new AuthUser
            {
                Username = dto.Username,
                PasswordHash = Hash(dto.Password)
            };

            _context.AuthUsers.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User Registered");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthDto dto)
        {
            var user = await _context.AuthUsers.FirstOrDefaultAsync(x => x.Username == dto.Username);
            if (user == null || user.PasswordHash != Hash(dto.Password))
                return BadRequest("Invalid credentials");

            return Ok(new { token = "sample-static-token", username = user.Username });
        }

        private string Hash(string password)
        {
            using var sha = SHA256.Create();
            return Convert.ToBase64String(sha.ComputeHash(Encoding.UTF8.GetBytes(password)));
        }
    }
}
