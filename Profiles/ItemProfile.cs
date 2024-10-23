using Althaus_Warehouse.Models.DTO.ItemDTOs;
using Althaus_Warehouse.Models.Entities;
using AutoMapper;

public class ItemProfile : Profile
{
    public ItemProfile()
    {
        // Mapping from Item to GetItemDTO
        CreateMap<Item, GetItemDTO>()
            .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => DateOnly.FromDateTime(src.DateCreated)))
            .ForMember(dest => dest.ItemType, opt => opt.MapFrom(src => src.ItemType != null ? new ItemTypeDTO
            {
                Id = src.ItemType.Id,
                Name = src.ItemType.Name,
                Description = src.ItemType.Description
            } : null))
            .ForMember(dest => dest.InStock, opt => opt.MapFrom(src => src.Quantity > 0));

        // Mapping from CreateItemDTO to Item
        CreateMap<CreateItemDTO, Item>()
            .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => DateTime.UtcNow)) // Automatically set creation date
            .ForMember(dest => dest.CreatedById, opt => opt.MapFrom(src => src.CreatedById)) // Map CreatedById
            .ForMember(dest => dest.ItemType, opt => opt.Ignore()) // Ignore the ItemType navigation property
            .AfterMap((src, dest) =>
            {
                dest.ItemTypeId = src.ItemTypeId; // Set ItemTypeId directly
            });

        // Mapping from UpdateItemDTO to Item
        CreateMap<UpdateItemDTO, Item>()
            .ForMember(dest => dest.CreatedById, opt => opt.Ignore()) // Ignore if not updating
            .ForMember(dest => dest.DateCreated, opt => opt.Ignore()) // Keep existing DateCreated
            .ForMember(dest => dest.ItemType, opt => opt.Ignore()) // Ignore ItemType navigation
            .ForMember(dest => dest.ItemTypeId, opt => opt.MapFrom(src => src.ItemTypeId)); // Directly map ItemTypeId
    }
}
