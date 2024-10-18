using System;
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
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the item to be added to inventory
        /// </summary>
        [Required]
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the price of the item
        /// </summary>
        [Required]
        public double Price { get; set; }

        /// <summary>
        /// Gets or sets the ID of the employee creating the item
        /// </summary>
        [Required]
        public int CreatedById { get; set; }
    }
}


/*
 * for later:
 * CreateMap<CreateItemDTO, Item>()
    .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => DateTime.Now)) // Set DateCreated automatically on create
    .ForMember(dest => dest.CreatedById, opt => opt.MapFrom(src => src.CreatedById)); // Map CreatedById

 * 
 */
