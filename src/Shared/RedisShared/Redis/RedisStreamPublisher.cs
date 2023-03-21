using RedisShared.Streaming;
using StackExchange.Redis;
using RedisShared.Serialization;

namespace RedisShared.Redis
{
    public sealed class RedisStreamPublisher : IStreamPublisher
    {
        private readonly ISerializer _serializer;
        private readonly ISubscriber _subscriber;

        public RedisStreamPublisher(IConnectionMultiplexer connectionMultiplexer, ISerializer serializer)
        {
            _serializer = serializer;
            _subscriber = connectionMultiplexer.GetSubscriber();
        }

        public Task PublishAsync<T>(string topic, T data) where T : class
        {
            var payload = _serializer.Serialize(data);
            return _subscriber.PublishAsync(topic, payload);
        }
    }
}
