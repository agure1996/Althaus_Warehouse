using Althaus_Warehouse.Models.Entities; // Assuming ItemType is defined in Entities
using System;
using Althaus_Warehouse.Models.DTO;

namespace Althaus_Warehouse.Models.DTO.ItemDTOs
{
    /// <summary>
    /// Data Transfer Object for getting item details.
    /// </summary>
    public class GetItemDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier for the item.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the item.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets a brief description of the item.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the current quantity of the item in stock.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the price of the item.
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Gets or sets the date the item was created in the system.
        /// </summary>
        public DateOnly DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the type/category of the item.
        /// </summary>
        public ItemTypeDTO? ItemType { get; set; } 

        /// <summary>
        /// Gets or sets stock status of item.
        /// </summary>
        public bool InStock { get; set; }
    }
}
