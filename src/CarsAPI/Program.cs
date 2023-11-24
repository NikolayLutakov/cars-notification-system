using Data;
using Microsoft.EntityFrameworkCore;
using CarsAPI.Services.BuisnesServices;
using CarsAPI.Services.DataServices;
using CarsAPI.Services.HostedServices;
using CarsAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers().AddNewtonsoftJson();
//builder.Services.AddEndpointsApiExplorer();

var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");

if (string.IsNullOrEmpty(connectionString))
{
    connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
}

builder.Services.AddDbContext<CarsContext>(opt =>
{
    opt.UseNpgsql(connectionString);
});

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddHttpClient();

builder.Services.AddScoped<ICarsDataService, CarsDataService>();
builder.Services.AddScoped<ITelegramNotificationDataService, TelegramNotificationDataService>();
builder.Services.AddScoped<ICarsService, CarsService>();
builder.Services.AddScoped<ITelegramMessageService, TelegramMessageService>();

builder.Services.AddHostedService<MonitorCarsHostedService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

app.Run();
