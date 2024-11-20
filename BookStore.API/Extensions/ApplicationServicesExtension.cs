using BookStore.API.Helpers;
using BookStore.Core;
using BookStore.Core.Repositories;
using BookStore.Repository;
using Microsoft.AspNetCore.Identity;

namespace BookStore.API.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
