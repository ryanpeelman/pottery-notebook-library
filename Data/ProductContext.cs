using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

public class ProductContext : DbContext
{
    public DbSet<ClayProduct>? Clays { get; set; }
    public DbSet<GlazeProduct>? Glazes { get; set; }

    public ProductContext(DbContextOptions<ProductContext> options) : base(options)
    {
        //Note: for debugging purposes, this should be removed
        SeedData();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("Product");
    }

    private void SeedData()
    {
        var clayData = File.ReadAllText("Data/clays.json");
        var clays = JsonConvert.DeserializeObject<List<ClayProduct>>(clayData) ?? new List<ClayProduct>();
        Clays?.AddRange(clays);

        var glazeData = File.ReadAllText("Data/glazes.json");
        var glazes = JsonConvert.DeserializeObject<List<GlazeProduct>>(glazeData) ?? new List<GlazeProduct>();
        Glazes?.AddRange(glazes);

        SaveChanges();
    }
}
