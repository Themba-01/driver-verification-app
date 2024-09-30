using Microsoft.EntityFrameworkCore;

namespace DriverVerificationBackend.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Verification> Verifications { get; set; }
    }
}
