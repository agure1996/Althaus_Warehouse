namespace Althaus_Warehouse.Models.DTO.ItemDTOs
{
    /// <summary>
    /// Data Transfer Object for listing items.
    /// </summary>
    public class ListItemsDTO
    {
        /// <summary>
        /// Gets or sets the unique identifier for the item.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the item.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the current quantity of the item in stock.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the price of the item.
        /// </summary>
        public double Price { get; set; }
    }
}
