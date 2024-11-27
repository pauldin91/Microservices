using Catalog.Api.Models;
using Marten.Schema;

namespace Catalog.Api.Data
{
    public class CatalogInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();
            if (await session.Query<Product>().AnyAsync())
                return;

            session.Store<Product>(GetPreconfiguredProducts());
            await session.SaveChangesAsync();
        }

        private IEnumerable<Product> GetPreconfiguredProducts() => new List<Product> {
            new Product{
                Id = Guid.NewGuid(),
                Name = "Test 1",
                Description = "Give a fuck",
                ImageFile="img.jpeg",
                Price=9.99M,
                Category=new List<string>{ "QA","QAA"}
            },
             new Product{
                Id = Guid.NewGuid(),
                Name = "Hakuna",
                Description = "Don't give a fuck",
                ImageFile="pumba.jpeg",
                Price=9.99M,
                Category=new List<string>{ "test","disney"}
            },
             new Product{
                Id = Guid.NewGuid(),
                Name = "Mattata",
                Description = "Don't give a fuck",
                ImageFile="timon.jpeg",
                Price=9.99M,
                Category=new List<string>{ "test","disney"}
            },
        };
    }
}