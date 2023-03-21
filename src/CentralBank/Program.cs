using CentralBank.Data;
using CentralBank.DataSynchronizer;
using CentralBank.Helpers;
using Microsoft.EntityFrameworkCore;
using RedisShared.Redis;
using RedisShared.Serialization;
using RedisShared.Streaming;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseInMemoryDatabase("InMemDb"));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IReferenceIndexRepo, ReferenceIndexRepo>();
builder.Services.AddRedis(builder.Configuration);
builder.Services.AddSerialization();
builder.Services.AddSingleton<IPublisher, Publisher>();
builder.Services.AddSingleton<IBankRateCalculator, BankRateCalculator>();
builder.Services.AddSingleton<IStreamPublisher, RedisStreamPublisher>();
builder.Services.AddSingleton<IStreamSubscriber, RedisStreamSubscriber>();

var app = builder.Build();
Console.WriteLine($"Is development: {app.Environment.IsDevelopment()}");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
