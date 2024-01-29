using Microsoft.Extensions.DependencyInjection;
using RatioMusic.Application.Abstracts;
using RatioMusic.Infrastructure.Implements;

namespace RatioMusic.Infrastructure.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationRepositoriesConfig(this IServiceCollection services)
        {            
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            return services;
        }
    }
}
