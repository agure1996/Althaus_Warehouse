using System.ComponentModel.DataAnnotations;

namespace Althaus_Warehouse.Models.DTO.ItemDTOs
{
    /// <summary>
    /// Data Transfer Object for updating an existing item.
    /// </summary>
    public class UpdateItemDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier for the item.
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the item.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets a brief description of the item.
        /// </summary>
        [MaxLength(200)]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the current quantity of the item in stock.
        /// </summary>
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a positive number.")]
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the price of the item.
        /// </summary>
        [Required]
        [Range(0.0, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public double Price { get; set; }

        /// <summary>
        /// Gets or sets the ID of the type/category of the item.
        /// </summary>
        [Required]
        public int ItemTypeId { get; set; } // Only keep the ItemTypeId
    }
}
