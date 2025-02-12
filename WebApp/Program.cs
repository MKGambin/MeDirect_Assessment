using CommonLibrary.Models;
using Microsoft.EntityFrameworkCore;
using WebApp;
using static CommonLibrary.Models.EnumsHelper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
));

builder.Services.AddSingleton<RabbitMQHelper>(provider =>
{
    var rabbitMQHelper = new RabbitMQHelper(configuration: provider.GetRequiredService<IConfiguration>(), queueType: QueueType.TradesProcess);

    rabbitMQHelper.InitializeAsync().GetAwaiter().GetResult();

    return rabbitMQHelper;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<MiddlewareErrorHandling>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
