using Microsoft.EntityFrameworkCore;

namespace endicott.Models
{
    public class EndicottContext : DbContext
    {
        public EndicottContext(DbContextOptions<EndicottContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
    }
}