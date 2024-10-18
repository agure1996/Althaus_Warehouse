using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        /// <param name="createdById">Id of the employee who created the item</param>
        public Item(int id, string name, string description, int quantity, double price, int createdById)
        {
            Id = id;
            Name = name;
            Description = description;
            Quantity = quantity;
            Price = price;
            CreatedById = createdById;
            DateCreated = DateOnly.FromDateTime(DateTime.Now);
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
        public double Price { get; set; }

        /// <summary>
        /// Gets or sets the ID of the employee who created the item
        /// </summary>
        public int? CreatedById { get; set; } // Nullable in case of import or bulk actions

        /// <summary>
        /// Navigation property to the Employee who created the item
        /// </summary>
        [ForeignKey("CreatedById")]
        public virtual Employee CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the date the item was created
        /// </summary>
        public DateOnly DateCreated { get; set; }  // Include DateCreated property
    }
}
