using System;
using Microsoft.Extensions.DependencyInjection;

namespace SuppliesPriceLister.AutoMapper
{
    /// <summary>
    /// Automapper Startup Service
    /// </summary>
    public static class AutoMapperSetup
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(typeof(AutoMapperConfig));
            AutoMapperConfig.RegisterMappings();
        }
    }
}
