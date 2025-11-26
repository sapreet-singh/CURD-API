using CURD_API.DataBase;
using CURD_API.Models;
using CURD_API.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CURD_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AppController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("Get")]
        public IActionResult GetUser()
        {
            var users = _context.Users.ToList();
            return Ok(users);

        }

        [HttpPost("Create")]
        public IActionResult CreateUser([FromBody] UserDto user)
        {
            var newUser = new User
            {
                Name = user.Name,
                age = user.Age,
                CreatedAt = DateTime.Now
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
            return Ok(newUser);
        }

        [HttpPut("Update/{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserDto user)
        {
            var existingUser = _context.Users.FirstOrDefault(x => x.Id == id);
            if (existingUser == null)
            {
                return NotFound();
            }
            existingUser.Name = user.Name;
            existingUser.age = user.Age;
            _context.SaveChanges();
            return Ok(existingUser);
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteUser(int id)
        {
            var existingUser = _context.Users.FirstOrDefault(x => x.Id == id);
            if (existingUser == null)
            {
                return NotFound();
            }
            _context.Users.Remove(existingUser);
            _context.SaveChanges();
            return Ok();
        }
    }
}
