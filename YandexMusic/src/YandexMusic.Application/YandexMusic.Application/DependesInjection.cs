using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Yandex.Shared.Service;
using Yandex.Shared.Service.lmpl;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using YandexMusic.Application.Services;
using YandexMusic.Application.Services.lmpl;
using YandexMusic.Application.Services.Impl;

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
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITarrifTypeService, TarrifTypeService>();
            services.AddScoped<IPaymentHistoryService, PaymentHistoryService>();
             services.AddScoped<IMusicService, MusicService>();
            services.AddScoped<IGenresService, GenreService>();
            services.AddScoped<ICardService, CardService>();
            //   services.AddScoped<IFavouriteService, FavouritiesService>();
            // services.AddScoped<IDowloandService,DowloandService>();
            services.AddScoped<ICard_TypeService, Card_TypeService>();
            services.AddScoped<IAuthorService,AuthorService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IClaimService, ClaimService>();
            services.AddScoped<TokenService, TokenService>();

         }

        private static void RegisterCaching(this IServiceCollection services)
        {
            // Регистрация кэша в памяти
            services.AddMemoryCache();
        }
     
    }
}
