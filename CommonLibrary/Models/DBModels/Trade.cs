using System.Text.Json.Serialization;
using static CommonLibrary.Models.EnumsHelper;

namespace CommonLibrary.Models.DBModels
{
    public class Trade
    {
        public int Id { get; set; }
        public string Identifier { get; set; } = $"{Guid.NewGuid()}";
        public string ConcurrencyStamp { get; set; } = $"{Guid.NewGuid()}";

        public int AppUserId { get; set; }

        [JsonIgnore]
        public AppUser? AppUser { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? ExecutedOn { get; set; }
        public TradeAction Action { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalAmount { get; set; }
        public TradeStatus Status { get; set; }
    }
}
