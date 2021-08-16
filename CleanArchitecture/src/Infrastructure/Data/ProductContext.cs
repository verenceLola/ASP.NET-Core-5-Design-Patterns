using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Models.Product> Products { get; set; }
    }
}
