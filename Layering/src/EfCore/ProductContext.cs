using Microsoft.EntityFrameworkCore;
using Layering.SharedModels;

namespace Layering.EfCore
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions options) : base(options) { }
        public DbSet<Product> Products { get; set; }
    }
}
