using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Althaus_Warehouse.Models.Entities
{
    public class Item
    {
        public Item() => DateCreated = DateTime.UtcNow;

        public Item(int id, string name, string description, int quantity, decimal price, int? createdById, int itemTypeId)
        {
            Id = id;
            Name = name;
            Description = description;
            Quantity = quantity;
            Price = price;
            CreatedById = createdById;
            ItemTypeId = itemTypeId;
            DateCreated = DateTime.UtcNow;

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }

        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int? CreatedById { get; set; }

        [ForeignKey("CreatedById")]
        public virtual Employee CreatedBy { get; set; }

        public DateTime DateCreated { get; set; }

        public int ItemTypeId { get; set; }

        public virtual ItemType ItemType { get; set; }

        [NotMapped]
        public bool InStock => Quantity > 0;
    }
}
