using Microsoft.EntityFrameworkCore;

namespace WebAppProductCart.Models
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions<ProjectContext> option): base(option)
        {
            
        }
        public DbSet<Product> tblProduct { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(new Product { ProductId = 1, Name = "test", Price = 200 },
                new Product { ProductId = 2, Name = "demo", Price = 800 });
        }
    }
}
