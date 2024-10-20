using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Althaus_Warehouse.Models.Entities
{
    /// <summary>
    /// Entity class representing an item category/type in the warehouse.
    /// </summary>
    public class ItemType
    {
        /// <summary>
        /// Gets or sets the unique identifier for the item type.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the item type/category.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }

        /// <summary>
        /// Optional: A brief description of the item type/category.
        /// </summary>
        [MaxLength(200)]
        public string? Description { get; set; }
    }
}
