using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Core.Repositaries;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Repositaries;

namespace Ordering.Infrastructure.Extensions
{
    public static class InfraServices
    {
        public static IServiceCollection AddInfraService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderContext>(options => options.UseSqlServer(configuration.GetConnectionString("OrderingConnectionString")));
            services.AddScoped(typeof(IAsyncRepositary<>), typeof(RepositaryBase<>));
            services.AddScoped<IOrderRepositary, OrderRepositary>();
            return services;
        }
    }
}
