using Althaus_Warehouse.Models.DTO.ItemDTOs;
using Althaus_Warehouse.Models.Entities;
using AutoMapper;

public class ItemProfile : Profile
{
    public ItemProfile()
    {
        /// <summary>
        /// Mapping from Item to GetItemDTO.
        /// Converts DateTime to DateOnly for DateCreated.
        /// Maps ItemType and checks if the item is in stock.
        /// </summary>
        CreateMap<Item, GetItemDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)) // Explicitly map Id
            .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.DateCreated)))
            .ForMember(dest => dest.ItemType, opt => opt.MapFrom(src => src.ItemType != null ? new ItemTypeDTO
            {
                Id = src.ItemType.Id,
                Name = src.ItemType.Name,
                Description = src.ItemType.Description
            } : null))
            .ForMember(dest => dest.InStock, opt => opt.MapFrom(src => src.Quantity > 0));


        /// <summary>
        /// Mapping from CreateItemDTO to Item.
        /// Automatically sets the creation date and maps CreatedById.
        /// Sets ItemTypeId after mapping.
        /// </summary>
        CreateMap<CreateItemDTO, Item>()
            .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => DateTime.UtcNow)) // Set creation date to now
            .ForMember(dest => dest.CreatedById, opt => opt.MapFrom(src => src.CreatedById)) // Map CreatedById
            .ForMember(dest => dest.ItemType, opt => opt.Ignore()) // Ignore ItemType to prevent issues
            .AfterMap((src, dest) =>
            {
                dest.ItemTypeId = src.ItemTypeId; // Set ItemTypeId after the mapping
            });

        /// <summary>
        /// Mapping from UpdateItemDTO to Item.
        /// Ignores CreatedById and DateCreated to keep existing values.
        /// Maps ItemTypeId from the DTO.
        /// </summary>
        CreateMap<UpdateItemDTO, Item>()
            .ForMember(dest => dest.CreatedById, opt => opt.Ignore()) // Keep existing CreatedById
            .ForMember(dest => dest.DateCreated, opt => opt.Ignore()) // Keep existing DateCreated
            .ForMember(dest => dest.ItemType, opt => opt.Ignore()) // Ignore ItemType to prevent issues
            .ForMember(dest => dest.ItemTypeId, opt => opt.MapFrom(src => src.ItemTypeId)); // Map ItemTypeId
    }
}
