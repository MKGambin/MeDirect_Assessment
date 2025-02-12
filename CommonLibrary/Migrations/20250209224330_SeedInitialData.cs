using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CommonLibrary.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "ConcurrencyStamp", "CreatedOn", "Email", "FirstName", "Identifier", "LastName" },
                values: new object[,]
                {
                    { 1, "d5f1b6e1-4d56-4327-9056-b69e8d3db6f1", new DateTime(2024, 12, 9, 3, 35, 18, 0, DateTimeKind.Unspecified), "jane.smith@mail.com", "Jane", "e3a4f2c6-63b8-4d21-9eb8-06545e8f1a91", "Smith" },
                    { 2, "f3d10a2c-9b26-48cf-b385-5e7f9b1e3c8a", new DateTime(2024, 12, 29, 0, 27, 29, 0, DateTimeKind.Unspecified), "john.doe@mail.com", "John", "b6a82c3b-8eb5-4b0c-b682-0f8e7c6d5a47", "Doe" },
                    { 3, "c4d27e3b-12a9-4f9e-81e6-f8a79b3d6c2d", new DateTime(2025, 1, 8, 17, 10, 59, 0, DateTimeKind.Unspecified), "bob.brown@mail.com", "Bob", "a2b97e46-7c5b-4485-b2a7-67e8c9f4d2b3", "Brown" },
                    { 4, "a7f6b3d2-8e4c-49a1-92f5-c7b6d3e8f2a1", new DateTime(2025, 1, 13, 3, 15, 10, 0, DateTimeKind.Unspecified), "charlie.white@mail.com", "Charlie", "9e3f4b6d-7b1c-4a28-a9f5-2b4c8d3e6f17", "White" },
                    { 5, "b9f5c7d3-6e4a-4b1f-82a7-3d2c8f4e1b7d", new DateTime(2025, 1, 14, 1, 21, 31, 0, DateTimeKind.Unspecified), "emma.green@mail.com", "Emma", "6b7d8e3c-4a1f-49a2-b9f5-1d3c7e8f2b6a", "Green" },
                    { 6, "c7d3b9f5-2e4a-4b1f-81a7-3d8c6f4e2b7d", new DateTime(2025, 1, 14, 18, 5, 58, 0, DateTimeKind.Unspecified), "david.black@mail.com", "David", "4e2b7d8f-3c6a-49a1-92f5-8d3e7b6c2f1a", "Black" },
                    { 7, "d3c7b9f5-8e4a-4b1f-81a7-2b8c6f4e7d3d", new DateTime(2025, 1, 30, 20, 22, 3, 0, DateTimeKind.Unspecified), "alice.johnson@mail.com", "Alice", "1b7d4e2c-8f6a-49a1-92f5-3d7c8b6f2e4a", "Johnson" }
                });

            migrationBuilder.InsertData(
                table: "Trades",
                columns: new[] { "Id", "Action", "AppUserId", "ConcurrencyStamp", "CreatedOn", "ExecutedOn", "Identifier", "Price", "Quantity", "Status", "TotalAmount" },
                values: new object[,]
                {
                    { 1, 2, 1, "d1be7005-0ca0-44ae-bdf9-e445ee111e56", new DateTime(2024, 12, 9, 8, 15, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 9, 9, 0, 0, 0, DateTimeKind.Unspecified), "e6b8c038-1b5c-4719-9317-7985920f1c0b", 160.00m, -1m, 4, -160.00m },
                    { 2, 1, 1, "12345d67-0285-4f4a-b536-b4a5ed94a75c", new DateTime(2024, 12, 12, 10, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 12, 11, 0, 0, 0, DateTimeKind.Unspecified), "e78db063-14c7-44c4-9b32-71a0b2d8a2b4", 266.00m, 1m, 3, 266.00m },
                    { 3, 2, 1, "885ff639-1d17-438d-92f0-3fc4a38e7f88", new DateTime(2024, 12, 12, 14, 45, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 12, 15, 30, 0, 0, DateTimeKind.Unspecified), "33bb3074-4c52-4f02-9536-44317ed4a6a3", 162.00m, -1m, 3, -162.00m },
                    { 4, 1, 2, "4df4c218-b5b0-49d9-8b02-9b6b10fc1a2b", new DateTime(2025, 1, 2, 9, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 2, 10, 0, 0, 0, DateTimeKind.Unspecified), "f9c1c4d1-1341-42cf-b927-871fa0a63d67", 150.00m, 1m, 3, 150.00m },
                    { 5, 2, 2, "4b6197b7-622f-4932-9b2d-3f243b1c15b3", new DateTime(2025, 1, 4, 10, 15, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 4, 11, 0, 0, 0, DateTimeKind.Unspecified), "98e2350b-9a56-42b0-a883-9ed3d1f7e831", 148.00m, -1m, 3, -148.00m },
                    { 6, 2, 3, "dc7b9b04-16ab-4790-8e6e-ff3e7ad9ca61", new DateTime(2025, 1, 8, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 8, 11, 0, 0, 0, DateTimeKind.Unspecified), "93bb0b7d-1f92-44d7-9be7-fc4f60c7d4e3", 145.00m, -1m, 4, -145.00m },
                    { 7, 1, 3, "6d71248a-e5f1-4d60-95c4-4ac1f1d7136b", new DateTime(2025, 1, 10, 13, 15, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 10, 14, 0, 0, 0, DateTimeKind.Unspecified), "8f17e1fc-022f-4c65-83a9-d7bb982d6175", 142.00m, 1m, 3, 142.00m },
                    { 8, 1, 7, "dfbd15b7-2b4d-4c48-bf26-90d9a3f60c92", new DateTime(2025, 1, 31, 8, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 31, 9, 30, 0, 0, DateTimeKind.Unspecified), "dff57d0c-bc91-40a3-b97f-5fbdfe48898b", 150.00m, 1m, 3, 150.00m },
                    { 9, 1, 7, "bc889cb3-cf64-4d22-b62e-b424c26db689", new DateTime(2025, 2, 2, 10, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 2, 2, 10, 30, 0, 0, DateTimeKind.Unspecified), "ab1cb59e-cc2f-4011-b139-e0d3f067f6b2", 155.00m, 1m, 3, 155.00m },
                    { 10, 1, 2, "8b28166f-8f31-4ed1-b83a-1dffb79ca098", new DateTime(2025, 2, 3, 14, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 2, 3, 14, 30, 0, 0, DateTimeKind.Unspecified), "ceac11db-4602-4e73-bd1b-b1db599241a6", 198.00m, 1m, 3, 198.00m },
                    { 11, 1, 1, "47fae6c8-e6b7-4774-b087-c9a22d508038", new DateTime(2025, 2, 5, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 2, 5, 9, 45, 0, 0, DateTimeKind.Unspecified), "a1b1b01b-ffec-40ea-b18a-5c711fd650be", 160.00m, 1m, 3, 160.00m },
                    { 12, 2, 3, "9787b0d9-b457-42d1-b1d7-80fe02858f7d", new DateTime(2025, 2, 6, 11, 15, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 2, 6, 11, 45, 0, 0, DateTimeKind.Unspecified), "3ca91f67-c8b1-463a-9de4-660b0a9c4187", 145.00m, -1m, 4, -145.00m },
                    { 13, 1, 7, "1ac8f717-0d39-45db-b85b-6f33e0b4d6e6", new DateTime(2025, 2, 7, 18, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 2, 7, 19, 0, 0, 0, DateTimeKind.Unspecified), "e11b0517-cbd4-4ed4-99bc-5fffd3c74c26", 155.00m, 1m, 3, 155.00m },
                    { 14, 1, 4, "a6e3ff48-5b96-43fa-8272-77c5c5c7f3f6", new DateTime(2025, 2, 8, 10, 45, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 2, 8, 11, 30, 0, 0, DateTimeKind.Unspecified), "c7359007-6a82-4e39-8d13-22c8ecb2ac80", 140.00m, 1m, 3, 140.00m },
                    { 15, 1, 5, "c56d62ea-cd06-476a-b1f7-28930769be91", new DateTime(2025, 2, 8, 16, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 2, 8, 17, 0, 0, 0, DateTimeKind.Unspecified), "b2764f66-5c72-4b69-b44c-7d244bde6078", 152.00m, 1m, 3, 152.00m },
                    { 16, 1, 2, "4ec2b2ba-bbdb-47a7-bbb6-4aeb2019e96a", new DateTime(2025, 2, 10, 10, 15, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 2, 10, 11, 0, 0, 0, DateTimeKind.Unspecified), "da9670b4-d45a-49b0-a383-b79db3e47a4f", 153.00m, 1m, 3, 153.00m },
                    { 17, 2, 6, "93db3d2c-c93c-4db2-babf-bf92e7e6da97", new DateTime(2025, 2, 11, 13, 15, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 2, 11, 14, 0, 0, 0, DateTimeKind.Unspecified), "d3d4ab3b-0e76-43e5-b37d-1fa1b3f1c989", 165.00m, -1m, 4, -165.00m },
                    { 18, 1, 5, "70b264a3-e79f-4a0c-b8e2-8e6f1a9d8fe5", new DateTime(2025, 2, 12, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 2, 12, 8, 30, 0, 0, DateTimeKind.Unspecified), "9c2f67cf-6267-4c31-9b5b-059f9f2787b9", 150.00m, 1m, 3, 150.00m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Trades",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Trades",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Trades",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Trades",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Trades",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Trades",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Trades",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Trades",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Trades",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Trades",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Trades",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Trades",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Trades",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Trades",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Trades",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Trades",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Trades",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Trades",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 7);
        }
    }
}
