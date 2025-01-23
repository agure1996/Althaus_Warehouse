using System.ComponentModel.DataAnnotations;

namespace Althaus_Warehouse.Models.DTO.ItemDTOs
{
    public class CreateItemDTO
    {
        [Required(ErrorMessage = "Item name is required.")]
        [MaxLength(50, ErrorMessage = "Item name cannot exceed 50 characters.")]
        public string? Name { get; set; }

        [MaxLength(200, ErrorMessage = "Description cannot exceed 200 characters.")]
        [Required(ErrorMessage = "Description is required.")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a non-negative number.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "The price must be greater than zero.")]
        public decimal Price { get; set; }

        public int? CreatedById { get; set; }

        [Required(ErrorMessage = "Item Type ID is required.")]
        public int ItemTypeId { get; set; }
    }
}
