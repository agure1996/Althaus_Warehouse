using System;

namespace Althaus_Warehouse.Models.DTO.ItemDTOs
{
    /// <summary>
    /// Data Transfer Object for retrieving item details
    /// </summary>
    public class GetItemDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier for the item
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the item
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets a brief description of the item
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the current quantity of the item in stock
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the price of the item
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Gets or sets the date the item was created
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the ID of the employee who created the item
        /// </summary>
        public int? CreatedById { get; set; }
    }
}
