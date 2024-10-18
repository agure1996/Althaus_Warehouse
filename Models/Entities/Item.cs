using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Althaus_Warehouse.Models.Entities
{
    /// <summary>
    /// Entity class representing an item in the warehouse.
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Default no-args constructor for Item.
        /// </summary>
        public Item()
        {
            DateCreated = DateOnly.FromDateTime(DateTime.Now);
        }

        /// <summary>
        /// Constructor for creating an item with all details.
        /// </summary>
        /// <param name="id">Unique identifier for the item.</param>
        /// <param name="name">The name of the item.</param>
        /// <param name="description">A brief description of the item.</param>
        /// <param name="quantity">The quantity of the item in stock.</param>
        /// <param name="price">The price of the item.</param>
        /// <param name="createdById">The ID of the employee who created the item.</param>
        /// <param name="itemType">The type/category of the item (e.g., Dairy, Electronics, etc.).</param>
        public Item(int id, string name, string description, int quantity, double price, int createdById, ItemType itemType)
        {
            Id = id;
            Name = name;
            Description = description;
            Quantity = quantity;
            Price = price;
            CreatedById = createdById;
            ItemType = itemType;
        }

        /// <summary>
        /// Gets or sets the unique identifier for the item.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the item.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a brief description of the item.
        /// </summary>
        [MaxLength(200)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the current quantity of the item in stock.
        /// </summary>
        [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a positive number.")]
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the price of the item.
        /// </summary>
        [Required]
        [Range(0.0, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public double Price { get; set; }

        /// <summary>
        /// Gets or sets the ID of the employee who created the item.
        /// Nullable in case the item was created during import or bulk actions.
        /// </summary>
        public int? CreatedById { get; set; }

        /// <summary>
        /// Navigation property to the Employee who created the item.
        /// </summary>
        [ForeignKey("CreatedById")]
        public virtual Employee CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date the item was created in the system.
        /// </summary>
        public DateOnly DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the type/category of the item (e.g., Dairy, Electronics, etc.).
        /// </summary>
        [Required]
        public ItemType ItemType { get; set; }
    }
}
