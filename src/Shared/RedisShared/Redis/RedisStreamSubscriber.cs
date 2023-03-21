﻿using RedisShared.Serialization;
using RedisShared.Streaming;
using StackExchange.Redis;

namespace RedisShared.Redis;

public sealed class RedisStreamSubscriber : IStreamSubscriber
{
    private readonly ISerializer _serializer;
    private readonly ISubscriber _subscriber;

    public RedisStreamSubscriber(IConnectionMultiplexer connectionMultiplexer, ISerializer serializer)
    {
        _serializer = serializer;
        _subscriber = connectionMultiplexer.GetSubscriber();
    }

    public Task SubscribeAsync<T>(string topic, Action<T> handler) where T : class
        => _subscriber.SubscribeAsync(topic, (_, data) =>
        {
            var payload = _serializer.Deserialize<T>(data);
            if (payload is null)
            {
                return;
            }

            handler(payload);
        });
}