using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

public class JsonProductContextFactory : IDbContextFactory<ProductContext>
{
    public ProductContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<ProductContext>()
            .UseInMemoryDatabase("Product")
            .Options;

        var context = new ProductContext(options);

        var clayData = File.ReadAllText("clays.json");
        var clays = JsonConvert.DeserializeObject<List<ClayProduct>>(clayData) ?? new List<ClayProduct>();
        context.Clays?.AddRange(clays);

        var glazeData = File.ReadAllText("glazes.json");
        var glazes = JsonConvert.DeserializeObject<List<GlazeProduct>>(glazeData) ?? new List<GlazeProduct>();
        context.Glazes?.AddRange(glazes);

        context.SaveChanges();

        return context;
    }
}
