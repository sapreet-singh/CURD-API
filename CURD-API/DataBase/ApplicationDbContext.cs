using CURD_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CURD_API.DataBase
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<AuthUser> AuthUsers { get; set; }
    }
}
