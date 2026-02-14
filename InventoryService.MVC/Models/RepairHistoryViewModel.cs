using System.Text.Json.Serialization;

namespace InventoryService.MVC.Models
{
    public class RepairHistoryViewModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("vehicleId")]
        public int VehicleId { get; set; }

        [JsonPropertyName("repairDate")]
        public DateTime RepairDate { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("cost")]
        public decimal Cost { get; set; }

        [JsonPropertyName("performedBy")]
        public string PerformedBy { get; set; } = string.Empty;
    }
}
