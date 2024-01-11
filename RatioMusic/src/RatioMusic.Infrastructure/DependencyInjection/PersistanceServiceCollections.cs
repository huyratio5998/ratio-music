using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RatioMusic.Infrastructure.Contexts;

namespace RatioMusic.Infrastructure.DependencyInjection
{
    public static class PersistanceServiceCollections
    {
        public static void AddRatioDBContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("RatioDBConnectionString");

            if (connectionString == null) return;

            services.AddDbContext<RatioMusicContext>(opt => opt.UseSqlServer(connectionString,
                builder => builder.MigrationsAssembly(typeof(RatioMusicContext).Assembly.FullName)));
        }
    }
}
