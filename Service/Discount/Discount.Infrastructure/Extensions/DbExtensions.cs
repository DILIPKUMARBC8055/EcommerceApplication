using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using System;
using System.Threading;

namespace Discount.Infrastructure.Extensions
{
    public static class DbExtensions
    {
        public static IHost MigrationDatabase<TContext>(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var config = services.GetRequiredService<IConfiguration>();
                var logger = services.GetRequiredService<ILogger<TContext>>();

                try
                {
                    logger.LogInformation("Discount DB Migration Started");
                    applyMigration(config, logger);
                    logger.LogInformation("Discount DB Migration Completed");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "An error occurred while migrating the Discount database.");
                    throw;
                }
            }

            return host;
        }

        private static void applyMigration(IConfiguration config, ILogger logger)
        {
            int retry = 5;
            while (retry > 0)
            {
                try
                {
                    // Get the connection string from the config
                    var connectionString = config.GetValue<string>("DatabaseSettings:ConnectionString");
                    using var connection = new NpgsqlConnection(connectionString);
                    connection.Open();  // Ensure connection is opened

                    using var cmd = new NpgsqlCommand();
                    cmd.Connection = connection;

                    logger.LogInformation("Running database migrations...");

                    // Drop the table if it exists
                    cmd.CommandText = "DROP TABLE IF EXISTS Coupon";
                    cmd.ExecuteNonQuery();

                    // Create the Coupon table
                    cmd.CommandText = @"CREATE TABLE Coupon(Id SERIAL PRIMARY KEY, 
                                                            ProductName VARCHAR(500) NOT NULL,
                                                            ProductDescription TEXT,
                                                            Amount INT)";
                    cmd.ExecuteNonQuery();

                    // Insert seed data
                    cmd.CommandText = "INSERT INTO Coupon(ProductName, ProductDescription, Amount) VALUES('Adidas Quick Force Indoor Badminton Shoes', 'Shoe Discount', 500);";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "INSERT INTO Coupon(ProductName, ProductDescription, Amount) VALUES('Yonex VCORE Pro 100 A Tennis Racquet (270gm, Strung)', 'Racquet Discount', 700);";
                    cmd.ExecuteNonQuery();

                    logger.LogInformation("Database migration completed successfully.");
                    break;  // Exit retry loop on success
                }
                catch (Exception ex)
                {
                    retry--;
                    logger.LogError(ex, $"Migration failed. Retrying... Attempts left: {retry}");

                    if (retry == 0)
                    {
                        logger.LogError("Maximum retry attempts reached. Migration failed.");
                        throw;  // Rethrow the exception after retries are exhausted
                    }

                    // Wait for 2 seconds before retrying
                    Thread.Sleep(2000);
                }
            }
        }
    }
}
