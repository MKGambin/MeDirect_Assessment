using Microsoft.EntityFrameworkCore;
using static CommonLibrary.Models.EnumsHelper;

namespace CommonLibrary.Models.DBModelsHelpers
{
    public class TradeHelper
    {
        public static async Task<bool> ValidTradeAsync(DataContext context, string identifier)
        {
            return await context.Trades.AnyAsync(x => x.Identifier == identifier);
        }

        public static async Task<List<int>> GetUsersWithInProgressTradesAsync(DataContext context)
        {
            return await context.Trades
                .Where(x => x.Status == TradeStatus.InProgress)
                .Select(x => x.AppUserId)
                .Distinct()
                .ToListAsync();
        }

        public static async Task<int?> GetEarliestPendingTradeAsync(DataContext context, IEnumerable<int>? excludedAppUserIds)
        {
            excludedAppUserIds ??= [];

            return await context.Trades
                .Where(x => !excludedAppUserIds.Contains(x.AppUserId) && x.Status == TradeStatus.Pending)
                .OrderBy(x => x.CreatedOn)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();
        }

        public static async Task UpdateTradeToInProgressAsync(DataContext context, int tradeId)
        {
            var dbItem = await context.Trades
                .FirstOrDefaultAsync(x => x.Id == tradeId && x.Status == TradeStatus.Pending) ?? throw new InvalidOperationException("Trade is invalid.");

            dbItem.Status = TradeStatus.InProgress;

            context.Trades.Update(dbItem);

            await context.SaveChangesAsync();
        }

        public static async Task ProcessTradeAsync(DataContext context, int tradeId)
        {
            var dbItem = await context.Trades
                .Include(x => x.AppUser)
                .FirstOrDefaultAsync(x => x.Id == tradeId && x.Status == TradeStatus.InProgress) ?? throw new InvalidOperationException("Trade is invalid.");

            switch (dbItem.Action)
            {
                case TradeAction.In:
                    {
                        dbItem.ExecutedOn = DateTime.UtcNow;
                        dbItem.Status = TradeStatus.Executed;

                        context.Trades.Update(dbItem);

                        await context.SaveChangesAsync();
                        return;
                    }
                case TradeAction.Out:
                    {
                        var appUserCurrentBalance = await AppUserHelper.GetAppUserCurrentBalanceAsync(context: context, identifier: dbItem.AppUser?.Identifier ?? "");

                        var appUserRemainingBalance = appUserCurrentBalance + dbItem.TotalAmount;
                        if (appUserRemainingBalance >= 0)
                        {
                            dbItem.ExecutedOn = DateTime.UtcNow;
                            dbItem.Status = TradeStatus.Executed;

                            context.Trades.Update(dbItem);

                            await context.SaveChangesAsync();
                            return;
                        }
                        break;
                    }
            }

            dbItem.Status = TradeStatus.Failed;

            context.Trades.Update(dbItem);

            await context.SaveChangesAsync();
            return;
        }

        public static async Task UpdateTradeToRetryAsync(DataContext context, RabbitMQHelper? rabbitMQHelper, string identifier)
        {
            var dbItem = await context.Trades
                .FirstOrDefaultAsync(x => x.Identifier == identifier && x.Status == TradeStatus.InProgress) ?? throw new InvalidOperationException("Trade is invalid.");

            dbItem.Status = TradeStatus.Pending;

            context.Trades.Update(dbItem);

            await context.SaveChangesAsync();

            if (rabbitMQHelper != null)
            {
                await rabbitMQHelper.PushMessageAsync(message: string.Empty);
            }
        }

        public static async Task UpdateTradeToCanceledAsync(DataContext context, RabbitMQHelper? rabbitMQHelper, string identifier)
        {
            var dbItem = await context.Trades
                .FirstOrDefaultAsync(x => x.Identifier == identifier && (x.Status == TradeStatus.Pending || x.Status == TradeStatus.InProgress)) ?? throw new InvalidOperationException("Trade is invalid.");

            dbItem.Status = TradeStatus.Canceled;

            context.Trades.Update(dbItem);

            await context.SaveChangesAsync();

            if (rabbitMQHelper != null)
            {
                await rabbitMQHelper.PushMessageAsync(message: string.Empty);
            }
        }
    }
}
