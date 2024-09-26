using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Discount.Infrastructure.Extensions
{
    public static class DbExtensions
    {
        public static IHost MigrationDatabase<TContext>(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var service = scope.ServiceProvider;
                var config = service.GetRequiredService<IConfiguration>();
                var logger = service.GetRequiredService<ILogger>();
                try
                {
                    logger.LogInformation("Migration started ");
                    applyMigration(config);
                    logger.LogInformation("Migration Completed");

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }

            }
            return host;
        }

        private static void applyMigration(IConfiguration config)
        {
            using var connection = new NpgsqlConnection(config.GetValue<string>("DatabaseSettings:ConnectionString"));
            using var cmd = new NpgsqlCommand();
            cmd.Connection = connection;

            cmd.CommandText = "DROP TABLE IF EXISTS Coupon";
            cmd.ExecuteNonQuery();
            cmd.CommandText = @"CREATE TABLE Coupon(Id SERIAL PRIMARY KEY, 
                                                    ProductName VARCHAR(500) NOT NULL,
                                                    ProductDescription TEXT,
                                                    Amount INT)";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO Coupon(ProductName, ProductDescription, Amount) VALUES('Adidas Quick Force Indoor Badminton Shoes', 'Shoe Discount', 500);";
            cmd.ExecuteNonQuery();

            cmd.CommandText = "INSERT INTO Coupon(ProductName, ProductDescription, Amount) VALUES('Yonex VCORE Pro 100 A Tennis Racquet (270gm, Strung)', 'Racquet Discount', 700);";
            cmd.ExecuteNonQuery();
        }
    }
}
