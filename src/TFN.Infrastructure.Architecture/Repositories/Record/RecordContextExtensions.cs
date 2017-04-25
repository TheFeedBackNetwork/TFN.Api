using System;
using Microsoft.Extensions.DependencyInjection;

namespace TFN.Infrastructure.Architecture.Repositories.Record
{
    public static class RecordContextExtensions
    {
        public static IServiceCollection AddRecordContext<TRecordContext>(this IServiceCollection services,
            Action<RecordSettings> configure)
            where TRecordContext : class
        {
            services.Configure(configure);

            return services.AddSingleton<TRecordContext>();
        }
    }
}