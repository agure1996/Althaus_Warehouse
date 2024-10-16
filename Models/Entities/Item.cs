using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Althaus_Warehouse.Models.Entities
{

    /// <summary>
    /// Entity class representing a warehouse item
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Non args constructor for Item
        /// </summary>
        public Item() { }

        /// <summary>
        /// Constructor for an item
        /// </summary>
        /// <param name="id">Item Id</param>
        /// <param name="name">Item name</param>
        /// <param name="description">Brief description of item</param>
        /// <param name="quantity">Current quantity of item</param>
        /// <param name="price">Item Price</param>
        public Item(int id, string name, string description, int quantity, int price)
        {
            Id = id;
            Name = name;
            Description = description;
            Quantity = quantity;
            Price = price;
        }

        /// <summary>
        /// Gets or sets the unique identifier for the item
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

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
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the current quantity of the item in stock
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the price of the item
        /// </summary>
        [Required]
        public int Price { get; set; }
    }
}
