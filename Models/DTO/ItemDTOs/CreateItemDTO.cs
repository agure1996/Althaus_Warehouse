using System.ComponentModel.DataAnnotations;

namespace Althaus_Warehouse.Models.DTO.ItemDTOs
{
    /// <summary>
    /// Data Transfer Object for creating a new item
    /// </summary>
    public class CreateItemDTO
    {
        /// <summary>
        /// Gets or sets the name of the item
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a brief description of the item
        /// </summary>
        [MaxLength(200)]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the current quantity of the item in stock
        /// </summary>
        [Required]
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the price of the item
        /// </summary>
        [Required]
        public double Price { get; set; }

        /// <summary>
        /// Date created in dd/MM/yyyy format
        /// </summary>
        public string DateCreated { get { return DateTime.Today.ToString("dd/MM/yyyy"); } }
    }

}
