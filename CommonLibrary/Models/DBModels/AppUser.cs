using System.Text.Json.Serialization;

namespace CommonLibrary.Models.DBModels
{
    public class AppUser
    {
        public int Id { get; set; }
        public string Identifier { get; set; } = $"{Guid.NewGuid()}";
        public string ConcurrencyStamp { get; set; } = $"{Guid.NewGuid()}";
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        [JsonIgnore]
        public List<Trade> Trades { get; set; } = [];
    }
}
