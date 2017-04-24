using System;
using Microsoft.Extensions.DependencyInjection;

namespace TFN.Infrastructure.Architecture.Repositories.Document
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDocumentDbContext<TDocumentContext>(this IServiceCollection services,
            Action<DocumentDbSettings> configure)
            where TDocumentContext : class
        {
            services.Configure(configure);

            return services.AddSingleton<TDocumentContext>();
        }
    }
}