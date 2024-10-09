using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Polly;

namespace Ordering.API.Extensions
{
    public static class DbExtensions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host, Action<TContext, IServiceProvider> seeder) where TContext : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var service = scope.ServiceProvider;
                var logger = service.GetRequiredService<ILogger<TContext>>();
                var context = service.GetRequiredService<TContext>();
                try
                {
                    logger.LogInformation("Database migration Started");
                    var retry = Policy.Handle<SqlException>()
                        .WaitAndRetry(
                            retryCount: 5,
                            sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                            onRetry: (exception, span, count) =>
                            {
                                logger.LogError($"Retrying because of {exception} {span}");
                            });
                    retry.Execute(() => callSeeder(seeder, context, service));
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"An Error occurred while migrating db: {typeof(TContext).Name}");
                    
                }

            }

            return host;
        }

        private static void callSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext context, IServiceProvider service) where TContext : DbContext
        {
            context.Database.Migrate();
            seeder(context, service);
        }
    }
}
