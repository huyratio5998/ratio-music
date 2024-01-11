using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RatioMusic.Application.Abstracts;
using RatioMusic.Application.Mapper;
using RatioMusic.Application.Services;
using RatioMusic.Application.ViewModels.Validations;

namespace RatioMusic.Application.DependencyInjection.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddConfigurationAutoMapper(this IServiceCollection services)
        {
            return services.AddAutoMapper(typeof(ServiceProfile));
        }

        public static IServiceCollection AddFluentValidation(this IServiceCollection services)
        {
            return services.AddValidatorsFromAssemblyContaining<SongApiRequestValidation>();
        }

        public static IServiceCollection AddApplicationServicesConfig(this IServiceCollection services)
        {
            services.AddScoped<ISongService, SongService>();
            services.AddScoped<IArtistService, ArtistService>();

            return services;
        }
    }
}
