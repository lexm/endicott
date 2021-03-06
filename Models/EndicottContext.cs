using Microsoft.EntityFrameworkCore;

namespace endicott.Models
{
    public class EndicottContext : DbContext
    {
        public EndicottContext(DbContextOptions<EndicottContext> options) : base(options) { }
        public DbSet<User> users { get; set; }
        public DbSet<Connect> connections { get; set; }
    }
}