using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace RedisShared.Redis
{
    public static class Connector
    {
        private const string RedisSection = "redis";

        public static IServiceCollection AddRedis(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetRequiredSection(RedisSection);
            var options = new RedisOptions();
            section.Bind(options);
            services.Configure<RedisOptions>(section);
            if (options.ConnectionString != null)
                services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(options.ConnectionString));

            return services; 
        }
    }
}
