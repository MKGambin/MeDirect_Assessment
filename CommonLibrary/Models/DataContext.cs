using CommonLibrary.Models.DBModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using static CommonLibrary.Models.EnumsHelper;

namespace CommonLibrary.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Trade> Trades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region DBModels
            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Id)
                    .ValueGeneratedOnAdd();

                entity.HasIndex(x => x.Identifier)
                    .IsUnique();

                entity.Property(x => x.ConcurrencyStamp)
                    .IsConcurrencyToken();
            });
            modelBuilder.Entity<Trade>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Id)
                    .ValueGeneratedOnAdd();

                entity.HasIndex(x => x.Identifier)
                    .IsUnique();

                entity.Property(x => x.ConcurrencyStamp)
                    .IsConcurrencyToken();

                entity
                    .HasOne(x => x.AppUser)
                    .WithMany(x => x.Trades)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            #endregion

            #region DBModels - SeedData
            modelBuilder.Entity<AppUser>().HasData(
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
                }
            );

            modelBuilder.Entity<Trade>().HasData(
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
                }
            );
            #endregion
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Modified)
                .ToList();

            foreach (var entityEntry in entities)
            {
                var dbItemEntity = entityEntry.Entity;
                var dbItemEntityType = dbItemEntity.GetType();

                var dbItemEntityId = dbItemEntityType.GetProperty("Id")?.GetValue(dbItemEntity);

                if (entityEntry.State == EntityState.Added)
                {
                    var identifierProperty = dbItemEntityType.GetProperty("Identifier");
                    if (identifierProperty != null && string.IsNullOrEmpty((string?)identifierProperty.GetValue(dbItemEntity)))
                    {
                        identifierProperty.SetValue(dbItemEntity, $"{Guid.NewGuid()}");
                    }

                    var concurrencyStampProperty = dbItemEntityType.GetProperty("ConcurrencyStamp");
                    if (concurrencyStampProperty != null && string.IsNullOrEmpty((string?)concurrencyStampProperty.GetValue(dbItemEntity)))
                    {
                        concurrencyStampProperty.SetValue(dbItemEntity, $"{Guid.NewGuid()}");
                    }
                }
                else if (entityEntry.State == EntityState.Modified)
                {
                    var concurrencyStampProperty = dbItemEntityType.GetProperty("ConcurrencyStamp");
                    if (concurrencyStampProperty != null)
                    {
                        var currentConcurrencyStamp = (string?)concurrencyStampProperty.GetValue(dbItemEntity);
                        if (string.IsNullOrEmpty(currentConcurrencyStamp)) { continue; }

                        bool validDBEntity = false;

                        switch (dbItemEntityType?.Name)
                        {
                            case nameof(AppUser):
                                {
                                    validDBEntity = await Set<AppUser>()
                                        .AsNoTracking()
                                        .AnyAsync(x => x.Id == (int?)dbItemEntityId && x.ConcurrencyStamp == currentConcurrencyStamp);
                                }
                                break;
                            case nameof(Trade):
                                {
                                    validDBEntity = await Set<Trade>()
                                        .AsNoTracking()
                                        .AnyAsync(x => x.Id == (int?)dbItemEntityId && x.ConcurrencyStamp == currentConcurrencyStamp);
                                }
                                break;
                            default:
                                throw new InvalidOperationException();
                        }

                        if (!validDBEntity)
                        {
                            throw new DbUpdateConcurrencyException("Concurrency conflict detected.");
                        }

                        concurrencyStampProperty.SetValue(dbItemEntity, $"{Guid.NewGuid()}");
                    }
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
