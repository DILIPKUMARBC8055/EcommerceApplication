using Catalog.Core.Entities;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.Text.Json;

namespace Catalog.Infrastructure.Data
{
    public static class BrandContextSeed
    {
        public static void SeedData(IMongoCollection<ProductBrand> brandCollection)
        {
            bool checkBrands = brandCollection.Find(b => true).Any();
            string path = Path.Combine("Data", "SeedData", "brands.json");
            Console.WriteLine(path);
            if (!checkBrands)
            {
                var brandsData = File.ReadAllText(path);
                //var brandsData = File.ReadAllText("../Catalog.Infrastructure/Data/SeedData/brands.json");
                var brands = JsonConvert.DeserializeObject<List<ProductBrand>>(brandsData);
                if (brands != null)
                {
                    foreach (var item in brands)
                    {
                        brandCollection.InsertOneAsync(item);
                    }
                }
            }
        }
    }
}