using CommonLibrary.Models;
using CommonLibrary.Models.DBModelsHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using static CommonLibrary.Models.EnumsHelper;

var _queueType = QueueType.TradesProcess;

var tradeCount = 0;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        config.SetBasePath(Directory.GetCurrentDirectory());
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        config.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);
        config.AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) =>
    {
        services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection")));

        services.AddSingleton<RabbitMQHelper>(provider =>
        {
            var rabbitMQHelper = new RabbitMQHelper(configuration: provider.GetRequiredService<IConfiguration>(), queueType: _queueType);

            rabbitMQHelper.InitializeAsync().GetAwaiter().GetResult();

            return rabbitMQHelper;
        });
    })
    .Build();

var _context = host.Services.GetRequiredService<DataContext>();

var _rabbitMQHelper = host.Services.GetRequiredService<RabbitMQHelper>();

await _rabbitMQHelper.InitializeAsync();
await _rabbitMQHelper.StartListeningAsync(ProcessTrade);

Console.WriteLine($"{_queueType} is running...");

Console.WriteLine("Press Esc to exit...");
while (true)
{
    var keyPress = Console.ReadKey(true);
    if (keyPress.Key == ConsoleKey.Escape)
    {
        break;
    }
}

async Task ProcessTrade(string message)
{
    try
    {
        var usersWithInProgressTrades = await TradeHelper.GetUsersWithInProgressTradesAsync(context: _context);

        var earliestPendingTrade = await TradeHelper.GetEarliestPendingTradeAsync(context: _context, excludedAppUserIds: usersWithInProgressTrades);
        if (earliestPendingTrade != null && earliestPendingTrade != 0)
        {
            Console.WriteLine($"Processing Trade: {message} (Counter - {++tradeCount})");

            await TradeHelper.UpdateTradeToInProgressAsync(context: _context, tradeId: (int)earliestPendingTrade);

            await TradeHelper.ProcessTradeAsync(context: _context, tradeId: (int)earliestPendingTrade);

            await _rabbitMQHelper.PushMessageAsync(message: string.Empty);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Failed to process trade.");
    }
}
