using Microsoft.EntityFrameworkCore;

public class ProductContext : DbContext
{
    public DbSet<ClayProduct>? Clays { get; set; }
    public DbSet<GlazeProduct>? Glazes { get; set; }

    public ProductContext(DbContextOptions<ProductContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("Product");
    }
}
