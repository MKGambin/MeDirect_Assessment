using CommonLibrary.Models;
using CommonLibrary.Models.DBModelsHelpers;
using Microsoft.EntityFrameworkCore;
using static CommonLibrary.Models.EnumsHelper;

namespace CommonLibrary.Tests
{
    public class TradeHelperTests
    {
        private DbContextOptions<DataContext> _dbContextOptions;
        private DataContext _context;

        [SetUp]
        public void SetUp()
        {
            _dbContextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: $"{Guid.NewGuid()}").Options;

            _context = new DataContext(options: _dbContextOptions);

            #region DBModels - SeedData
            _context.AppUsers.AddRange(
                new()
                {
                    Id = 1,
                    Identifier = "e3a4f2c6-63b8-4d21-9eb8-06545e8f1a91",
                    ConcurrencyStamp = "d5f1b6e1-4d56-4327-9056-b69e8d3db6f1",
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "jane.smith@mail.com",
                    CreatedOn = new DateTime(2024, 12, 09, 03, 35, 18),
                },
                new()
                {
                    Id = 2,
                    Identifier = "b6a82c3b-8eb5-4b0c-b682-0f8e7c6d5a47",
                    ConcurrencyStamp = "f3d10a2c-9b26-48cf-b385-5e7f9b1e3c8a",
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@mail.com",
                    CreatedOn = new DateTime(2024, 12, 29, 00, 27, 29),
                },
                new()
                {
                    Id = 3,
                    Identifier = "a2b97e46-7c5b-4485-b2a7-67e8c9f4d2b3",
                    ConcurrencyStamp = "c4d27e3b-12a9-4f9e-81e6-f8a79b3d6c2d",
                    FirstName = "Bob",
                    LastName = "Brown",
                    Email = "bob.brown@mail.com",
                    CreatedOn = new DateTime(2025, 01, 08, 17, 10, 59),
                },
                new()
                {
                    Id = 4,
                    Identifier = "9e3f4b6d-7b1c-4a28-a9f5-2b4c8d3e6f17",
                    ConcurrencyStamp = "a7f6b3d2-8e4c-49a1-92f5-c7b6d3e8f2a1",
                    FirstName = "Charlie",
                    LastName = "White",
                    Email = "charlie.white@mail.com",
                    CreatedOn = new DateTime(2025, 01, 13, 03, 15, 10),
                },
                new()
                {
                    Id = 5,
                    Identifier = "6b7d8e3c-4a1f-49a2-b9f5-1d3c7e8f2b6a",
                    ConcurrencyStamp = "b9f5c7d3-6e4a-4b1f-82a7-3d2c8f4e1b7d",
                    FirstName = "Emma",
                    LastName = "Green",
                    Email = "emma.green@mail.com",
                    CreatedOn = new DateTime(2025, 01, 14, 01, 21, 31),
                },
                new()
                {
                    Id = 6,
                    Identifier = "4e2b7d8f-3c6a-49a1-92f5-8d3e7b6c2f1a",
                    ConcurrencyStamp = "c7d3b9f5-2e4a-4b1f-81a7-3d8c6f4e2b7d",
                    FirstName = "David",
                    LastName = "Black",
                    Email = "david.black@mail.com",
                    CreatedOn = new DateTime(2025, 01, 14, 18, 05, 58),
                },
                new()
                {
                    Id = 7,
                    Identifier = "1b7d4e2c-8f6a-49a1-92f5-3d7c8b6f2e4a",
                    ConcurrencyStamp = "d3c7b9f5-8e4a-4b1f-81a7-2b8c6f4e7d3d",
                    FirstName = "Alice",
                    LastName = "Johnson",
                    Email = "alice.johnson@mail.com",
                    CreatedOn = new DateTime(2025, 01, 30, 20, 22, 03),
                });
            _context.SaveChanges();

            _context.Trades.AddRange(
                new()
                {
                    Id = 1,
                    Identifier = "e6b8c038-1b5c-4719-9317-7985920f1c0b",
                    ConcurrencyStamp = "d1be7005-0ca0-44ae-bdf9-e445ee111e56",
                    AppUserId = 1,
                    CreatedOn = new DateTime(2024, 12, 09, 08, 15, 00),
                    ExecutedOn = new DateTime(2024, 12, 09, 09, 00, 00),
                    Action = TradeAction.Out,
                    Quantity = -1,
                    Price = 160.00m,
                    TotalAmount = -160.00m,
                    Status = TradeStatus.Failed,
                },
                new()
                {
                    Id = 2,
                    Identifier = "e78db063-14c7-44c4-9b32-71a0b2d8a2b4",
                    ConcurrencyStamp = "12345d67-0285-4f4a-b536-b4a5ed94a75c",
                    AppUserId = 1,
                    CreatedOn = new DateTime(2024, 12, 12, 10, 30, 00),
                    ExecutedOn = new DateTime(2024, 12, 12, 11, 00, 00),
                    Action = TradeAction.In,
                    Quantity = 1,
                    Price = 266.00m,
                    TotalAmount = 266.00m,
                    Status = TradeStatus.Executed,
                },
                new()
                {
                    Id = 3,
                    Identifier = "33bb3074-4c52-4f02-9536-44317ed4a6a3",
                    ConcurrencyStamp = "885ff639-1d17-438d-92f0-3fc4a38e7f88",
                    AppUserId = 1,
                    CreatedOn = new DateTime(2024, 12, 12, 14, 45, 00),
                    ExecutedOn = new DateTime(2024, 12, 12, 15, 30, 00),
                    Action = TradeAction.Out,
                    Quantity = -1,
                    Price = 162.00m,
                    TotalAmount = -162.00m,
                    Status = TradeStatus.Executed,
                },
                new()
                {
                    Id = 4,
                    Identifier = "f9c1c4d1-1341-42cf-b927-871fa0a63d67",
                    ConcurrencyStamp = "4df4c218-b5b0-49d9-8b02-9b6b10fc1a2b",
                    AppUserId = 2,
                    CreatedOn = new DateTime(2025, 01, 02, 09, 30, 00),
                    ExecutedOn = new DateTime(2025, 01, 02, 10, 00, 00),
                    Action = TradeAction.In,
                    Quantity = 1,
                    Price = 150.00m,
                    TotalAmount = 150.00m,
                    Status = TradeStatus.Executed,
                },
                new()
                {
                    Id = 5,
                    Identifier = "98e2350b-9a56-42b0-a883-9ed3d1f7e831",
                    ConcurrencyStamp = "4b6197b7-622f-4932-9b2d-3f243b1c15b3",
                    AppUserId = 2,
                    CreatedOn = new DateTime(2025, 01, 04, 10, 15, 00),
                    ExecutedOn = new DateTime(2025, 01, 04, 11, 00, 00),
                    Action = TradeAction.Out,
                    Quantity = -1,
                    Price = 148.00m,
                    TotalAmount = -148.00m,
                    Status = TradeStatus.Executed,
                },
                new()
                {
                    Id = 6,
                    Identifier = "93bb0b7d-1f92-44d7-9be7-fc4f60c7d4e3",
                    ConcurrencyStamp = "dc7b9b04-16ab-4790-8e6e-ff3e7ad9ca61",
                    AppUserId = 3,
                    CreatedOn = new DateTime(2025, 01, 08, 10, 00, 00),
                    ExecutedOn = new DateTime(2025, 01, 08, 11, 00, 00),
                    Action = TradeAction.Out,
                    Quantity = -1,
                    Price = 145.00m,
                    TotalAmount = -145.00m,
                    Status = TradeStatus.Failed,
                },
                new()
                {
                    Id = 7,
                    Identifier = "8f17e1fc-022f-4c65-83a9-d7bb982d6175",
                    ConcurrencyStamp = "6d71248a-e5f1-4d60-95c4-4ac1f1d7136b",
                    AppUserId = 3,
                    CreatedOn = new DateTime(2025, 01, 10, 13, 15, 00),
                    ExecutedOn = new DateTime(2025, 01, 10, 14, 00, 00),
                    Action = TradeAction.In,
                    Quantity = 1,
                    Price = 142.00m,
                    TotalAmount = 142.00m,
                    Status = TradeStatus.Executed,
                },
                new()
                {
                    Id = 8,
                    Identifier = "dff57d0c-bc91-40a3-b97f-5fbdfe48898b",
                    ConcurrencyStamp = "dfbd15b7-2b4d-4c48-bf26-90d9a3f60c92",
                    AppUserId = 7,
                    CreatedOn = new DateTime(2025, 01, 31, 08, 30, 00),
                    ExecutedOn = new DateTime(2025, 01, 31, 09, 30, 00),
                    Action = TradeAction.In,
                    Quantity = 1,
                    Price = 150.00m,
                    TotalAmount = 150.00m,
                    Status = TradeStatus.Executed,
                },
                new()
                {
                    Id = 9,
                    Identifier = "ab1cb59e-cc2f-4011-b139-e0d3f067f6b2",
                    ConcurrencyStamp = "bc889cb3-cf64-4d22-b62e-b424c26db689",
                    AppUserId = 7,
                    CreatedOn = new DateTime(2025, 02, 02, 10, 00, 00),
                    ExecutedOn = new DateTime(2025, 02, 02, 10, 30, 00),
                    Action = TradeAction.In,
                    Quantity = 1,
                    Price = 155.00m,
                    TotalAmount = 155.00m,
                    Status = TradeStatus.Executed,
                },
                new()
                {
                    Id = 10,
                    Identifier = "ceac11db-4602-4e73-bd1b-b1db599241a6",
                    ConcurrencyStamp = "8b28166f-8f31-4ed1-b83a-1dffb79ca098",
                    AppUserId = 2,
                    CreatedOn = new DateTime(2025, 02, 03, 14, 00, 00),
                    ExecutedOn = new DateTime(2025, 02, 03, 14, 30, 00),
                    Action = TradeAction.In,
                    Quantity = 1,
                    Price = 198.00m,
                    TotalAmount = 198.00m,
                    Status = TradeStatus.Executed,
                },
                new()
                {
                    Id = 11,
                    Identifier = "a1b1b01b-ffec-40ea-b18a-5c711fd650be",
                    ConcurrencyStamp = "47fae6c8-e6b7-4774-b087-c9a22d508038",
                    AppUserId = 1,
                    CreatedOn = new DateTime(2025, 02, 05, 09, 00, 00),
                    ExecutedOn = new DateTime(2025, 02, 05, 09, 45, 00),
                    Action = TradeAction.In,
                    Quantity = 1,
                    Price = 160.00m,
                    TotalAmount = 160.00m,
                    Status = TradeStatus.Executed,
                },
                new()
                {
                    Id = 12,
                    Identifier = "3ca91f67-c8b1-463a-9de4-660b0a9c4187",
                    ConcurrencyStamp = "9787b0d9-b457-42d1-b1d7-80fe02858f7d",
                    AppUserId = 3,
                    CreatedOn = new DateTime(2025, 02, 06, 11, 15, 00),
                    ExecutedOn = new DateTime(2025, 02, 06, 11, 45, 00),
                    Action = TradeAction.Out,
                    Quantity = -1,
                    Price = 145.00m,
                    TotalAmount = -145.00m,
                    Status = TradeStatus.Failed,
                },
                new()
                {
                    Id = 13,
                    Identifier = "e11b0517-cbd4-4ed4-99bc-5fffd3c74c26",
                    ConcurrencyStamp = "1ac8f717-0d39-45db-b85b-6f33e0b4d6e6",
                    AppUserId = 7,
                    CreatedOn = new DateTime(2025, 02, 07, 18, 30, 00),
                    ExecutedOn = new DateTime(2025, 02, 07, 19, 00, 00),
                    Action = TradeAction.In,
                    Quantity = 1,
                    Price = 155.00m,
                    TotalAmount = 155.00m,
                    Status = TradeStatus.Executed,
                },
                new()
                {
                    Id = 14,
                    Identifier = "c7359007-6a82-4e39-8d13-22c8ecb2ac80",
                    ConcurrencyStamp = "a6e3ff48-5b96-43fa-8272-77c5c5c7f3f6",
                    AppUserId = 4,
                    CreatedOn = new DateTime(2025, 02, 08, 10, 45, 00),
                    ExecutedOn = new DateTime(2025, 02, 08, 11, 30, 00),
                    Action = TradeAction.In,
                    Quantity = 1,
                    Price = 140.00m,
                    TotalAmount = 140.00m,
                    Status = TradeStatus.Executed,
                },
                new()
                {
                    Id = 15,
                    Identifier = "b2764f66-5c72-4b69-b44c-7d244bde6078",
                    ConcurrencyStamp = "c56d62ea-cd06-476a-b1f7-28930769be91",
                    AppUserId = 5,
                    CreatedOn = new DateTime(2025, 02, 08, 16, 30, 00),
                    ExecutedOn = new DateTime(2025, 02, 08, 17, 00, 00),
                    Action = TradeAction.In,
                    Quantity = 1,
                    Price = 152.00m,
                    TotalAmount = 152.00m,
                    Status = TradeStatus.Executed,
                },
                new()
                {
                    Id = 16,
                    Identifier = "da9670b4-d45a-49b0-a383-b79db3e47a4f",
                    ConcurrencyStamp = "4ec2b2ba-bbdb-47a7-bbb6-4aeb2019e96a",
                    AppUserId = 2,
                    CreatedOn = new DateTime(2025, 02, 10, 10, 15, 00),
                    ExecutedOn = new DateTime(2025, 02, 10, 11, 00, 00),
                    Action = TradeAction.In,
                    Quantity = 1,
                    Price = 153.00m,
                    TotalAmount = 153.00m,
                    Status = TradeStatus.Executed,
                },
                new()
                {
                    Id = 17,
                    Identifier = "d3d4ab3b-0e76-43e5-b37d-1fa1b3f1c989",
                    ConcurrencyStamp = "93db3d2c-c93c-4db2-babf-bf92e7e6da97",
                    AppUserId = 6,
                    CreatedOn = new DateTime(2025, 02, 11, 13, 15, 00),
                    ExecutedOn = new DateTime(2025, 02, 11, 14, 00, 00),
                    Action = TradeAction.Out,
                    Quantity = -1,
                    Price = 165.00m,
                    TotalAmount = -165.00m,
                    Status = TradeStatus.Failed,
                },
                new()
                {
                    Id = 18,
                    Identifier = "9c2f67cf-6267-4c31-9b5b-059f9f2787b9",
                    ConcurrencyStamp = "70b264a3-e79f-4a0c-b8e2-8e6f1a9d8fe5",
                    AppUserId = 5,
                    CreatedOn = new DateTime(2025, 02, 12, 08, 00, 00),
                    ExecutedOn = new DateTime(2025, 02, 12, 08, 30, 00),
                    Action = TradeAction.In,
                    Quantity = 1,
                    Price = 150.00m,
                    TotalAmount = 150.00m,
                    Status = TradeStatus.Executed,
                },
                new()
                {
                    Id = 19,
                    Identifier = "e6b8c038-1b5c-4719-9317-7985920f1c0b",
                    ConcurrencyStamp = "d1be7005-0ca0-44ae-bdf9-e445ee111e56",
                    AppUserId = 3,
                    CreatedOn = new DateTime(2025, 02, 11, 02, 05, 00),
                    ExecutedOn = null,
                    Action = TradeAction.In,
                    Quantity = 1,
                    Price = 150.00m,
                    TotalAmount = 150.00m,
                    Status = TradeStatus.Canceled,
                },
                new()
                {
                    Id = 20,
                    Identifier = "bc4bcac8-59be-43f3-8138-35031ffb036f",
                    ConcurrencyStamp = "4f1e8069-2056-40d0-bb99-e3135f597dbb",
                    AppUserId = 3,
                    CreatedOn = new DateTime(2025, 02, 11, 03, 25, 00),
                    ExecutedOn = null,
                    Action = TradeAction.In,
                    Quantity = 1,
                    Price = 150.00m,
                    TotalAmount = 150.00m,
                    Status = TradeStatus.Failed,
                },
                new()
                {
                    Id = 21,
                    Identifier = "a0dfe618-5092-47a9-9f85-d1f1ffb0e9d3",
                    ConcurrencyStamp = "5b6064ad-e6fa-4fd2-81e9-7fdcb60c4c77",
                    AppUserId = 7,
                    CreatedOn = new DateTime(2025, 02, 11, 03, 15, 00),
                    ExecutedOn = null,
                    Action = TradeAction.In,
                    Quantity = 1,
                    Price = 160.00m,
                    TotalAmount = 160.00m,
                    Status = TradeStatus.Pending,
                },
                new()
                {
                    Id = 22,
                    Identifier = "db5c4bfc-b06a-41c4-b50c-bf71d819d1a1",
                    ConcurrencyStamp = "09287235-61a1-40a1-a64c-04b7f3496170",
                    AppUserId = 7,
                    CreatedOn = new DateTime(2025, 02, 11, 03, 30, 00),
                    ExecutedOn = null,
                    Action = TradeAction.Out,
                    Quantity = -1,
                    Price = 150.00m,
                    TotalAmount = -150.00m,
                    Status = TradeStatus.Pending,
                },
                new()
                {
                    Id = 23,
                    Identifier = "d8b4c23d-cbe1-442b-8b99-f3c4a9a99e9e",
                    ConcurrencyStamp = "246ad35b-e045-4f6b-b755-e76a41fe57a3",
                    AppUserId = 5,
                    CreatedOn = new DateTime(2025, 02, 11, 03, 35, 00),
                    ExecutedOn = null,
                    Action = TradeAction.Out,
                    Quantity = -1,
                    Price = decimal.MaxValue,
                    TotalAmount = -decimal.MaxValue,
                    Status = TradeStatus.Pending,
                });
            _context.SaveChanges();
            #endregion
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        #region ValidTradeAsync
        [Test]
        public async Task ValidTradeAsync_ShouldReturnTrue_WhenTradeExists()
        {
            var dbItem = await _context.Trades.FirstOrDefaultAsync(x => x.Id == 1) ?? new();

            var result = await TradeHelper.ValidTradeAsync(context: _context, identifier: dbItem.Identifier);

            Assert.That(result, Is.True);
        }

        [Test]
        public async Task ValidTradeAsync_ShouldReturnFalse_WhenTradeDoesNotExist()
        {
            var dbItem = await _context.Trades.FirstOrDefaultAsync(x => x.Id == -1) ?? new();

            var result = await TradeHelper.ValidTradeAsync(context: _context, identifier: dbItem.Identifier);

            Assert.That(result, Is.False);
        }
        #endregion
        #region GetUsersWithInProgressTradesAsync
        [Test]
        public async Task GetUsersWithInProgressTradesAsync_ShouldReturnCorrectUsers()
        {
            var result = await TradeHelper.GetUsersWithInProgressTradesAsync(context: _context);

            Assert.That(result, Is.Empty);
        }
        #endregion
        #region GetEarliestPendingTradeAsync
        [Test]
        public async Task GetEarliestPendingTradeAsync_ShouldReturnNull_WhenNoPendingTrades()
        {
            _context.Trades.RemoveRange(_context.Trades.Where(x => x.Status == TradeStatus.Pending));
            await _context.SaveChangesAsync();

            var result = await TradeHelper.GetEarliestPendingTradeAsync(_context, excludedAppUserIds: null);

            Assert.That(result, Is.Zero);
        }

        [Test]
        public async Task GetEarliestPendingTradeAsync_ShouldReturnEarliestTradeId_WhenThereArePendingTrades()
        {
            var dbItem = await _context.Trades.Where(x => x.Status == TradeStatus.Pending).OrderBy(x => x.CreatedOn).FirstOrDefaultAsync();

            var result = await TradeHelper.GetEarliestPendingTradeAsync(_context, excludedAppUserIds: null);

            Assert.That(result, Is.EqualTo(dbItem?.Id ?? -1));
        }

        [Test]
        public async Task GetEarliestPendingTradeAsync_ShouldReturnNull_WhenAllUsersExcluded()
        {
            var result = await TradeHelper.GetEarliestPendingTradeAsync(_context, excludedAppUserIds: [.. _context.AppUsers.Select(x => x.Id)]);

            Assert.That(result, Is.Zero);
        }

        [Test]
        public async Task GetEarliestPendingTradeAsync_ShouldReturnCorrectTrade_WhenExcludingSpecificUsers()
        {
            var pendingTrades = await _context.Trades.Where(x => x.Status == TradeStatus.Pending).ToListAsync();
            var pendingTradesGroupedByAppUser = pendingTrades.GroupBy(x => new { AppUserId = x.AppUserId });

            var excludeAppUserId = pendingTradesGroupedByAppUser.ElementAt(0).First().Id;

            var earliestPendingTradeId = pendingTrades.Where(x => x.AppUserId != excludeAppUserId).OrderBy(x => x.CreatedOn).FirstOrDefault()?.Id ?? -1;

            var result = await TradeHelper.GetEarliestPendingTradeAsync(_context, excludedAppUserIds: [excludeAppUserId]);

            Assert.That(result, Is.EqualTo(earliestPendingTradeId));
        }
        #endregion
        #region UpdateTradeToInProgressAsync
        [Test]
        public async Task UpdateTradeToInProgressAsync_ShouldUpdateStatus_WhenTradeIsPending()
        {
            var dbItemOld = await _context.Trades.FirstOrDefaultAsync(x => x.Status == TradeStatus.Pending);
            var dbItemOldId = dbItemOld?.Id ?? -1;

            await TradeHelper.UpdateTradeToInProgressAsync(context: _context, tradeId: dbItemOldId);

            var dbItem = await _context.Trades.FirstOrDefaultAsync(x => x.Id == dbItemOldId);

            Assert.That(dbItem?.Status, Is.EqualTo(TradeStatus.InProgress));
        }

        [TestCase(arg: -1, Description = "Invalid Record")]
        [TestCase(arg: 18, Description = "Status- Executed")]
        [TestCase(arg: 19, Description = "Status- Canceled")]
        [TestCase(arg: 20, Description = "Status- Failed")]
        public async Task UpdateTradeToInProgressAsync_ShouldThrowInvalidOperationException_WhenTradeNotFound(int tradeId)
        {
            try
            {
                await TradeHelper.UpdateTradeToInProgressAsync(context: _context, tradeId: tradeId);
            }
            catch (Exception ex)
            {
                Assert.That(ex.Message, Is.EqualTo("Trade is invalid."));
            }
        }
        #endregion
        #region ProcessTradeAsync
        [TestCase(arg: -1, Description = "Invalid Record")]
        [TestCase(arg: 18, Description = "Status- Executed")]
        [TestCase(arg: 19, Description = "Status- Canceled")]
        [TestCase(arg: 20, Description = "Status- Failed")]
        [TestCase(arg: 21, Description = "Status- Pending")]
        public async Task ProcessTradeAsync_ShouldThrowInvalidOperationException_WhenTradeNotFound(int tradeId)
        {
            try
            {
                await TradeHelper.ProcessTradeAsync(context: _context, tradeId: tradeId);
            }
            catch (Exception ex)
            {
                Assert.That(ex.Message, Is.EqualTo("Trade is invalid."));
            }
        }

        [Test]
        public async Task ProcessTradeAsync_ShouldUpdateStatusToExecuted_WhenActionIsIn()
        {
            var tradeId = 21;

            await TradeHelper.UpdateTradeToInProgressAsync(context: _context, tradeId: tradeId);

            await TradeHelper.ProcessTradeAsync(context: _context, tradeId: tradeId);

            var dbItem = await _context.Trades.FirstOrDefaultAsync(x => x.Id == tradeId);

            Assert.That(dbItem, Is.Not.Null);
            Assert.That(dbItem.ExecutedOn, Is.Not.Null);
            Assert.That(dbItem.Status, Is.EqualTo(TradeStatus.Executed));
        }

        [Test]
        public async Task ProcessTradeAsync_ShouldUpdateStatusToExecuted_WhenActionIsOutAndBalanceIsSufficient()
        {
            var tradeId = 22;

            await TradeHelper.UpdateTradeToInProgressAsync(context: _context, tradeId: tradeId);

            await TradeHelper.ProcessTradeAsync(context: _context, tradeId: tradeId);

            var dbItem = await _context.Trades.FirstOrDefaultAsync(x => x.Id == tradeId);

            Assert.That(dbItem, Is.Not.Null);
            Assert.That(dbItem.ExecutedOn, Is.Not.Null);
            Assert.That(dbItem.Status, Is.EqualTo(TradeStatus.Executed));
        }

        [Test]
        public async Task ProcessTradeAsync_ShouldUpdateStatusToFailed_WhenActionIsOutAndBalanceIsInsufficient()
        {
            var tradeId = 23;

            await TradeHelper.UpdateTradeToInProgressAsync(context: _context, tradeId: tradeId);

            await TradeHelper.ProcessTradeAsync(context: _context, tradeId: tradeId);

            var dbItem = await _context.Trades.FirstOrDefaultAsync(x => x.Id == tradeId);

            Assert.That(dbItem, Is.Not.Null);
            Assert.That(dbItem.Status, Is.EqualTo(TradeStatus.Failed));
        }
        #endregion
        #region UpdateTradeToRetryAsync
        [TestCase(arg: -1, Description = "Invalid Record")]
        [TestCase(arg: 18, Description = "Status- Executed")]
        [TestCase(arg: 19, Description = "Status- Canceled")]
        [TestCase(arg: 20, Description = "Status- Failed")]
        [TestCase(arg: 21, Description = "Status- Pending")]
        public async Task UpdateTradeToRetryAsync_ShouldThrowInvalidOperationException_WhenTradeNotFound(int tradeId)
        {
            try
            {
                var dbItem = await _context.Trades.FirstOrDefaultAsync(x => x.Id == tradeId);
                var dbItemIdentifier = dbItem?.Identifier ?? "";

                await TradeHelper.UpdateTradeToRetryAsync(context: _context, rabbitMQHelper: null, identifier: dbItemIdentifier);
            }
            catch (Exception ex)
            {
                Assert.That(ex.Message, Is.EqualTo("Trade is invalid."));
            }
        }

        [Test]
        public async Task UpdateTradeToRetryAsync_ShouldUpdateStatusToPending_WhenTradeFoundWithInProgressStatus()
        {
            var tradeId = 21;
            var tradeIdentifier = "a0dfe618-5092-47a9-9f85-d1f1ffb0e9d3";

            await TradeHelper.UpdateTradeToInProgressAsync(context: _context, tradeId: tradeId);

            await TradeHelper.UpdateTradeToRetryAsync(context: _context, rabbitMQHelper: null, identifier: tradeIdentifier);

            var dbItem = await _context.Trades.FirstOrDefaultAsync(x => x.Id == tradeId);

            Assert.That(dbItem, Is.Not.Null);
            Assert.That(dbItem.Status, Is.EqualTo(TradeStatus.Pending));
        }
        #endregion
        #region UpdateTradeToCanceledAsync
        [TestCase(arg: -1, Description = "Invalid Record")]
        [TestCase(arg: 18, Description = "Status- Executed")]
        [TestCase(arg: 19, Description = "Status- Canceled")]
        [TestCase(arg: 20, Description = "Status- Failed")]
        public async Task UpdateTradeToCanceledAsync_ShouldThrowInvalidOperationException_WhenTradeNotFound(int tradeId)
        {
            try
            {
                var dbItem = await _context.Trades.FirstOrDefaultAsync(x => x.Id == tradeId);
                var dbItemIdentifier = dbItem?.Identifier ?? "";

                await TradeHelper.UpdateTradeToCanceledAsync(context: _context, rabbitMQHelper: null, identifier: dbItemIdentifier);
            }
            catch (Exception ex)
            {
                Assert.That(ex.Message, Is.EqualTo("Trade is invalid."));
            }
        }

        [Test]
        public async Task UpdateTradeToCanceledAsync_ShouldUpdateStatusToCanceled_WhenTradeFoundWithPendingStatus()
        {
            var tradeId = 21;
            var tradeIdentifier = "a0dfe618-5092-47a9-9f85-d1f1ffb0e9d3";

            await TradeHelper.UpdateTradeToCanceledAsync(context: _context, rabbitMQHelper: null, identifier: tradeIdentifier);

            var dbItem = await _context.Trades.FirstOrDefaultAsync(x => x.Id == tradeId);

            Assert.That(dbItem, Is.Not.Null);
            Assert.That(dbItem.Status, Is.EqualTo(TradeStatus.Canceled));
        }

        [Test]
        public async Task UpdateTradeToCanceledAsync_ShouldUpdateStatusToCanceled_WhenTradeFoundWithInProgressStatus()
        {
            var tradeId = 21;
            var tradeIdentifier = "a0dfe618-5092-47a9-9f85-d1f1ffb0e9d3";

            await TradeHelper.UpdateTradeToInProgressAsync(context: _context, tradeId: tradeId);

            await TradeHelper.UpdateTradeToCanceledAsync(context: _context, rabbitMQHelper: null, identifier: tradeIdentifier);

            var dbItem = await _context.Trades.FirstOrDefaultAsync(x => x.Id == tradeId);

            Assert.That(dbItem, Is.Not.Null);
            Assert.That(dbItem.Status, Is.EqualTo(TradeStatus.Canceled));
        }
        #endregion
    }
}
