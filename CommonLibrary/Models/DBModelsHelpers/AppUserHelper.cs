using CommonLibrary.Models.DBModels;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;
using static CommonLibrary.Models.EnumsHelper;

namespace CommonLibrary.Models.DBModelsHelpers
{
    public class AppUserHelper
    {
        public static async Task<bool> ValidAppUserAsync(DataContext context, string identifier)
        {
            return await context.AppUsers.AnyAsync(x => x.Identifier == identifier);
        }

        public static async Task<object> GetAppUserDetailsAsync(DataContext context, string identifier)
        {
            var appUser = await context.AppUsers
                .FirstOrDefaultAsync(x => x.Identifier == identifier) ?? throw new InvalidOperationException("AppUser is invalid.");

            var appUserCurrentBalance = await GetAppUserCurrentBalanceAsync(context: context, identifier: identifier);

            var appUserLastTradeExecuted = await GetAppUserLastExecutedTradeAsync(context: context, identifier: identifier);

            dynamic appUserDetails = new ExpandoObject();
            appUserDetails.Identifier = appUser.Identifier;
            appUserDetails.FirstName = appUser.FirstName;
            appUserDetails.LastName = appUser.LastName;
            appUserDetails.Email = appUser.Email;
            appUserDetails.CreatedOn = $"{appUser.CreatedOn:dd-MM-yyyy}";
            appUserDetails.CurrentBalance = $"{appUserCurrentBalance:F2}";
            appUserDetails.LastTradeExecutedOn = $"{appUserLastTradeExecuted?.ExecutedOn:dd-MM-yyyy}";

            return appUserDetails;
        }

        public static async Task<decimal> GetAppUserCurrentBalanceAsync(DataContext context, string identifier)
        {
            var appUser = await context.AppUsers
                .FirstOrDefaultAsync(x => x.Identifier == identifier) ?? throw new InvalidOperationException("AppUser is invalid.");

            var currentBalance = await context.Trades
                .Where(x => x.AppUserId == appUser.Id && x.Status == TradeStatus.Executed)
                .SumAsync(x => x.TotalAmount);

            return currentBalance;
        }

        public static async Task<Trade?> GetAppUserLastExecutedTradeAsync(DataContext context, string identifier)
        {
            var appUser = await context.AppUsers
                .FirstOrDefaultAsync(x => x.Identifier == identifier) ?? throw new InvalidOperationException("AppUser is invalid.");

            return await context.Trades
                .Where(x => x.AppUserId == appUser.Id && x.Status == TradeStatus.Executed)
                .OrderByDescending(x => x.ExecutedOn)
                .FirstOrDefaultAsync();
        }

        public static async Task<object> GetAppUserTradesAsync(DataContext context, string identifier, int page = 1, int pageSize = 10)
        {
            var appUser = await context.AppUsers
                .FirstOrDefaultAsync(x => x.Identifier == identifier) ?? throw new InvalidOperationException("AppUser is invalid.");

            var appUserTrades = context.Trades
                .Where(x => x.AppUserId == appUser.Id)
                .AsQueryable();

            var appUserTradesCount = await appUserTrades.CountAsync();

            var appUserViewingTrades = await appUserTrades
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            dynamic appUserTradesList = new ExpandoObject();
            appUserTradesList.TotalTrades = appUserTradesCount;
            appUserTradesList.Page = page;
            appUserTradesList.PageSize = pageSize;
            appUserTradesList.TotalPages = (int)Math.Ceiling(appUserTradesCount / (double)pageSize);
            appUserTradesList.Trades = appUserViewingTrades.Select(x =>
            {
                dynamic tradeDetails = new ExpandoObject();
                tradeDetails.Identifier = x.Identifier;
                tradeDetails.CreatedOn = $"{x.CreatedOn:dd-MM-yyyy}";
                tradeDetails.ExecutedOn =
                    x.ExecutedOn != null ? $"{x.ExecutedOn:dd-MM-yyyy}"
                    : null;
                tradeDetails.Action = $"{x.Action}";
                tradeDetails.Quantity = $"{x.Quantity}";
                tradeDetails.Price = $"{x.Price:F2}";
                tradeDetails.TotalAmount = $"{x.TotalAmount:F2}";
                tradeDetails.Status = $"{x.Status}";

                return tradeDetails;
            });

            return appUserTradesList;
        }

        public static async Task CreateTrade(DataContext context, RabbitMQHelper? rabbitMQHelper, string identifier, TradeAction tradeAction, decimal price)
        {
            var appUser = await context.AppUsers
                .FirstOrDefaultAsync(x => x.Identifier == identifier) ?? throw new InvalidOperationException("AppUser is invalid.");

            var dbItem = new Trade();
            dbItem.AppUserId = appUser.Id;
            dbItem.CreatedOn = DateTime.UtcNow;
            dbItem.Action = tradeAction;
            dbItem.Quantity =
                tradeAction.Equals(TradeAction.Out) ? -1
                : 1;
            dbItem.Price = price;
            dbItem.TotalAmount = dbItem.Quantity * dbItem.Price;
            dbItem.Status = TradeStatus.Pending;

            context.Trades.Add(dbItem);

            await context.SaveChangesAsync();

            if (rabbitMQHelper != null)
            {
                await rabbitMQHelper.PushMessageAsync(message: string.Empty);
            }
        }
    }
}
