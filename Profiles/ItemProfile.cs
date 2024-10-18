using AutoMapper;
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
            /// <summary>
            /// Mapping from Item to GetItemDTO
            /// </summary>
            CreateMap<Item, GetItemDTO>();

            /// <summary>
            /// Mapping from CreateItemDTO to Item
            /// </summary>
            CreateMap<CreateItemDTO, Item>();

            /// <summary>
            /// Mapping from UpdateItemDTO to Item
            /// </summary>
            CreateMap<UpdateItemDTO, Item>();

            /// <summary>
            /// When creating an item, set the DateHired property to the current date
            /// </summary>
            CreateMap<CreateItemDTO, Item>()
                .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => DateTime.Today));
        }
    }
}
