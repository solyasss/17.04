using Microsoft.EntityFrameworkCore;

namespace _17._04.Models
{
    public class user_context : DbContext
    {
        public DbSet<user_model> users { get; set; }
        public DbSet<message_model> messages { get; set; }

        public user_context(DbContextOptions<user_context> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}