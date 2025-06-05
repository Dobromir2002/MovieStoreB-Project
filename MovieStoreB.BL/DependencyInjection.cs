
using Microsoft.Extensions.DependencyInjection;
using MovieStoreB.BL.Interfaces;
using MovieStoreB.BL.Services;
using MovieStoreB.Infrastructure.Interfaces;
using MovieStoreB.Infrastructure.Services;

namespace MovieStoreB.BL
{
    public static class DependencyInjection
    {
        public static IServiceCollection 
            AddBusinessDependencies(this IServiceCollection services)
        {
            services.AddSingleton<MovieService, MovieService>();
            services.AddSingleton<IBlMovieService, BlMovieService>();
            services.AddSingleton<IExternalMovieApiService, ExternalMovieApiService>();

            services.AddSingleton<FakeDatabaseService>();
            services.AddSingleton<ICacheDistributorService, CacheDistributorService>();
            services.AddSingleton<ICacheConsumerService, CacheConsumerService>();

            services.AddHttpClient<ExternalMovieApiService>();

            return services;
        }
    }
}
