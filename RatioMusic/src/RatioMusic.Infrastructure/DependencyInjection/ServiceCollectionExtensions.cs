using Microsoft.Extensions.DependencyInjection;
using RatioMusic.Application.Abstracts;
using RatioMusic.Infrastructure.Implements;

namespace RatioMusic.Infrastructure.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationRepositoriesConfig(this IServiceCollection services)
        {
            services.AddScoped<ISongRepository, SongRepository>();
            services.AddScoped<IArtistRepository, ArtistRepository>();

            return services;
        }
    }
}
