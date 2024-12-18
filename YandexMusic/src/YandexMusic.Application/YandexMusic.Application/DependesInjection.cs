using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Yandex.Shared.Service;
using Yandex.Shared.Service.lmpl;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;

namespace YandexMusic.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services,  IWebHostEnvironment env)
        {
            services.AddServices(env);
            services.RegisterCaching();
            return services;
        }

        private static void AddServices(this IServiceCollection services, IWebHostEnvironment env)
        {
            // Регистрация зависимостей сервисов
            services.AddScoped<IClaimService, ClaimService>();
        }

        private static void RegisterCaching(this IServiceCollection services)
        {
            // Регистрация кэша в памяти
            services.AddMemoryCache();
        }
    }
}
