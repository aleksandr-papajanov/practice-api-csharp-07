using Microsoft.EntityFrameworkCore;
using Movie.API.Services;
using Movie.Contracts;
using Movie.Core.Contracts;
using Movie.Core.Contracts.Repositories;
using Movie.Data;
using Movie.Data.Repositories;
using Movie.Services;

namespace Movie.API.Extentions
{
    internal static class WebApplicationExtensions
    {
        public static void AddAppDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("AppDbContext")
                    ?? throw new InvalidOperationException("Connection string 'MovieContext' not found.")));
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            services.AddScoped<IFilmRepository, FilmRepository>();
            services.AddScoped<IFilmGenreRepository, FilmGenreRepository>();
            services.AddScoped<IFilmDetailsRepository, FilmDetailsRepository>();
            services.AddScoped<IActorRepository, ActorRepository>();
            services.AddScoped<IFilmActorRepository, FilmActorRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();

            services.AddScoped(provider => new Lazy<IFilmRepository>(() => provider.GetRequiredService<IFilmRepository>()));
            services.AddScoped(provider => new Lazy<IFilmGenreRepository>(() => provider.GetRequiredService<IFilmGenreRepository>()));
            services.AddScoped(provider => new Lazy<IFilmDetailsRepository>(() => provider.GetRequiredService<IFilmDetailsRepository>()));
            services.AddScoped(provider => new Lazy<IActorRepository>(() => provider.GetRequiredService<IActorRepository>()));
            services.AddScoped(provider => new Lazy<IFilmActorRepository>(() => provider.GetRequiredService<IFilmActorRepository>()));
            services.AddScoped(provider => new Lazy<IReviewRepository>(() => provider.GetRequiredService<IReviewRepository>()));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IFilmService, FilmService>();
            services.AddScoped<IActorService, ActorService>();
            services.AddScoped<IReviewService, ReviewService>();

            services.AddScoped(provider => new Lazy<IFilmService>(() => provider.GetRequiredService<IFilmService>()));
            services.AddScoped(provider => new Lazy<IActorService>(() => provider.GetRequiredService<IActorService>()));
            services.AddScoped(provider => new Lazy<IReviewService>(() => provider.GetRequiredService<IReviewService>()));

            services.AddScoped<IServiceManager, ServiceManager>();
        }

        public static void AddHostedServices(this IServiceCollection services)
        {
            services.AddHostedService<DataSeedingService>();
            services.AddHostedService<ReviewCleanupService>();
        }
    }
}
