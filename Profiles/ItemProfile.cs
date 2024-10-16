﻿using AutoMapper;
using Althaus_Warehouse.Models.Entities;
using Althaus_Warehouse.Models.DTO.ItemDTOs;

namespace Althaus_Warehouse.MappingProfiles
{
    /// <summary>
    /// Mapping profile for the Item entity and Item DTOs
    /// </summary>
    public class ItemProfile : Profile
    {
        /// <summary>
        /// Constructor to define the mappings between Entity and DTOs
        /// </summary>
        public ItemProfile()
        {
            // Mapping from Item to GetItemDTO (read operations)
            CreateMap<Item, GetItemDTO>();

            // Mapping from Item to ListItemsDTO (for listing items)
            CreateMap<Item, ListItemsDTO>();

            // Mapping from CreateItemDTO to Item (create operations)
            CreateMap<CreateItemDTO, Item>()
                .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => DateTime.Today)) // Set DateCreated automatically
                .ForMember(dest => dest.CreatedById, opt => opt.MapFrom(src => src.CreatedById)); // Map CreatedById

            // Mapping from UpdateItemDTO to Item (update operations)
            CreateMap<UpdateItemDTO, Item>();

            // Optional: Reverse mapping for UpdateItemDTO if needed for flexibility
            CreateMap<Item, UpdateItemDTO>().ReverseMap();
        }
    }
}
