using Catalog.Core.Entities;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace Catalog.Infrastructure.Data
{
    public static class TypeContextSeed
    {
        public static void SeedData(IMongoCollection<ProductType> typeCollection)
        {
            bool checkTypes = typeCollection.Find(b => true).Any();
            string basePath = Path.Combine("..","src", "Service", "Catalog", "Catalog.Infrastructure");
            string path = Path.Combine(basePath, "Data", "SeedData", "types.json");
            if (!checkTypes)
            {
                var typesData = File.ReadAllText(path);
               // var typesData = File.ReadAllText("../Catalog.Infrastructure/Data/SeedData/types.json");
                var types = JsonConvert.DeserializeObject<List<ProductType>>(typesData);
                if (types != null)
                {
                    foreach (var item in types)
                    {
                        typeCollection.InsertOneAsync(item);
                    }
                }
            }
        }
    }
}