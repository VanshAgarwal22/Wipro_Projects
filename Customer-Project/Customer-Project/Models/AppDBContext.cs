using Microsoft.EntityFrameworkCore;

namespace Customer_Project.Models   // Namespace Models ka hi rakh
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customer { get; set; }
    }
}
