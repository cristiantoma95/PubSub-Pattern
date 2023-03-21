using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace RedisShared.Serialization
{
    internal sealed class SystemTextJsonSerializer : ISerializer
    {
        private static readonly JsonSerializerOptions Options = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
        };

        public string Serialize<T>(T value) where T : class => JsonSerializer.Serialize(value, Options);

        public T? Deserialize<T>(string value) where T : class => JsonSerializer.Deserialize<T>(value, Options);
    }
}
