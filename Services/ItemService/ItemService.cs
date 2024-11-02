using Althaus_Warehouse.Models.DTO.ItemDTOs;
using Althaus_Warehouse.Models.Entities;
using Althaus_Warehouse.Services.Repositories;
using AutoMapper; // Include AutoMapper for DTO to Entity mapping

namespace Althaus_Warehouse.Services.ItemService
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IItemTypeRepository _itemTypeRepository;
        private readonly IMapper _mapper; // AutoMapper instance

        public ItemService(IItemRepository itemRepository, IItemTypeRepository itemTypeRepository, IMapper mapper)
        {
            _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
            _itemTypeRepository = itemTypeRepository ?? throw new ArgumentNullException(nameof(itemTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); // Inject AutoMapper
        }

        public async Task<Item?> GetItemByIdAsync(int itemId) => await _itemRepository.GetItemByIdAsync(itemId);

        public async Task<(IEnumerable<Item> Items, int TotalCount)> GetAllItemsAsync(int pageSize, int currentPage)
        {
            return await _itemRepository.GetAllItemsAsync(pageSize, currentPage);
        }

        public async Task<List<ItemType>> GetAllItemTypesAsync()
        {
            var itemTypes = await _itemTypeRepository.GetAllItemTypesAsync();
            return (List<ItemType>)(itemTypes ?? new List<ItemType>());
        }

        public async Task<Item> CreateItemAsync(CreateItemDTO itemDTO)
        {
            // Map CreateItemDTO to Item entity
            var item = _mapper.Map<Item>(itemDTO);
            item.DateCreated = DateTime.UtcNow; // Set creation date

            // Add the item to the repository
            await _itemRepository.CreateItemAsync(item);
            await _itemRepository.SaveChangesAsync(); // Save changes to the database

            return item; // Return the created item or any relevant DTO
        }

        public async Task UpdateItemAsync(int itemId, UpdateItemDTO itemDTO)
        {
            var item = await GetItemByIdAsync(itemId);
            if (item == null)
                throw new KeyNotFoundException($"Item with ID {itemId} not found.");

            var itemType = await _itemTypeRepository.GetItemTypeByIdAsync(itemDTO.ItemTypeId);
            if (itemType == null)
            {
                throw new KeyNotFoundException($"ItemType with ID {itemDTO.ItemTypeId} not found.");
            }

            item.Name = itemDTO.Name;
            item.Description = itemDTO.Description;
            item.Quantity = itemDTO.Quantity;
            item.Price = itemDTO.Price;
            item.ItemTypeId = itemDTO.ItemTypeId;
            item.ItemType = itemType;  // Set the ItemType entity explicitly

            await _itemRepository.UpdateItemAsync(item);
            await _itemRepository.SaveChangesAsync(); // Finalize the update operation
        }

        public async Task DeleteItemAsync(int itemId)
        {
            await _itemRepository.DeleteItemAsync(itemId);
            await _itemRepository.SaveChangesAsync(); // Save changes
        }
    }
}
