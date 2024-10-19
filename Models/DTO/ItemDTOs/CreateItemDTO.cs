using System.ComponentModel.DataAnnotations;

namespace Althaus_Warehouse.Models.DTO.ItemDTOs
{
    /// <summary>
    /// Data Transfer Object for creating a new item.
    /// </summary>
    public class CreateItemDTO
    {
        /// <summary>
        /// Gets or sets the name of the item.
        /// </summary>
        [Required(ErrorMessage = "Item name is required.")]
        [MaxLength(50, ErrorMessage = "Item name cannot exceed 50 characters.")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a brief description of the item.
        /// </summary>
        [MaxLength(200, ErrorMessage = "Description cannot exceed 200 characters.")]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the current quantity of the item in stock.
        /// </summary>
        [Required(ErrorMessage = "Quantity is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a non-negative number.")]
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the price of the item.
        /// </summary>
        [Required(ErrorMessage = "Price is required.")]
        [Range(0.0, double.MaxValue, ErrorMessage = "Price must be a non-negative number.")]
        public decimal Price { get; set; }  // Changed from double to decimal

        /// <summary>
        /// Gets or sets the ID of the employee who created the item.
        /// Nullable for bulk imports or other use cases.
        /// </summary>
        public int? CreatedById { get; set; }

        /// <summary>
        /// Gets or sets the foreign key of the type/category of the item.
        /// </summary>
        [Required(ErrorMessage = "Item Type ID is required.")]
        public int ItemTypeId { get; set; }
    }
}
