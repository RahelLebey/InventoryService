
using System.ComponentModel.DataAnnotations;

namespace InventoryService.Models
{
    public class Inventory
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(80)]
        public string ItemName { get; set; } = string.Empty;

        [Range(0, 100000)]
        public int Quantity { get; set; }

        [Range(0.0, 1000000.0)]
        public decimal Price { get; set; }

        [MaxLength(200)]
        public string? Location { get; set; }
    }
}
