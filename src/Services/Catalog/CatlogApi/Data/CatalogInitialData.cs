using Marten.Schema;

namespace CatlogApi.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();

        if(await session.Query<Product>().AnyAsync())
        {
            return;
        }


        session.Store<Product>(GetPreconfiguredProducts());

        await session.SaveChangesAsync();
    }

    private static IEnumerable<Product> GetPreconfiguredProducts()
    {

        return new List<Product>{ 
            new Product {
            
                Id = new Guid("f38ce661-6cc5-440e-9555-1e47b0e36f4a"),
                Name = "IPhone",
                Description = "........",
                ImageFile = "img",
                Price = 950,
                Category = new List<string> { "Smart Phone"}

            
            },
            
            new Product {
            
                Id = new Guid("f38ce661-6cc5-440e-9555-1e47b0e36f4b"),
                Name = "Samsung 10",
                Description = "...... ",
                ImageFile = "img",
                Price = 890,
                Category = new List<string> { "Smart Phone"}

            
            },
            
            new Product {
            
                Id = new Guid("f38ce631-6cc5-440e-9555-1e47b0e36f5b"),
                Name = "Samsung A24",
                Description = "...... ",
                ImageFile = "img",
                Price = 590,
                Category = new List<string> { "Smart Phone"}

            
            },
        
        };

    }
}


