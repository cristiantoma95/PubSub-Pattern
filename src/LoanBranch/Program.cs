using LoanBranch.Services;
using RedisShared.Redis;
using RedisShared.Serialization;
using RedisShared.Streaming;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHostedService<SubscriberService>();
builder.Services.AddSerialization();
builder.Services.AddRedis(builder.Configuration);
builder.Services.AddSingleton<IStreamSubscriber, RedisStreamSubscriber>();
var app = builder.Build();

app.Run();
