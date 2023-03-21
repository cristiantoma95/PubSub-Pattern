using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace RedisShared.Serialization
{
    public static class Serialization
    {
        public static IServiceCollection AddSerialization(this IServiceCollection services)
            => services.AddSingleton<ISerializer, SystemTextJsonSerializer>();
    }
}
