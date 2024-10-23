using Althaus_Warehouse.Models.DTO.ItemDTOs;
using Althaus_Warehouse.Models.Entities;
using Althaus_Warehouse.Services.Repositories;

namespace Althaus_Warehouse.Services.ItemService
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
        }


        public async Task<Item?> GetItemByIdAsync(int itemId)
        {
            return await _itemRepository.GetItemByIdAsync(itemId);
        }

        public async Task<(IEnumerable<Item> Items, int TotalCount)> GetAllItemsAsync(int pageSize, int currentPage)
        {
            return await _itemRepository.GetAllItemsAsync(pageSize, currentPage);
        }




        public async Task CreateItemAsync(Item item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            await _itemRepository.AddItemAsync(item);
        }

        public async Task UpdateItemAsync(int itemId, UpdateItemDTO itemDTO)
        {
            var item = await GetItemByIdAsync(itemId);
            if (item == null) throw new KeyNotFoundException($"Item with ID {itemId} not found.");

            // Update properties based on the DTO
            item.Name = itemDTO.Name;
            item.Description = itemDTO.Description;
            item.Quantity = itemDTO.Quantity;
            item.Price = itemDTO.Price;
            item.ItemTypeId = itemDTO.ItemTypeId; // Assuming you have item types

            await _itemRepository.UpdateItemAsync(item);
        }

        public async Task DeleteItemAsync(int itemId)
        {
            await _itemRepository.DeleteItemAsync(itemId);
        }
    }
}
