using Althaus_Warehouse.Models.DTO.ItemDTOs;
using Althaus_Warehouse.Models.Entities;
using Althaus_Warehouse.Services.Repositories;

namespace Althaus_Warehouse.Services.ItemService
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IItemTypeRepository _itemTypeRepository;

        public ItemService(IItemRepository itemRepository , IItemTypeRepository itemTypeRepository)
        {
            _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
            _itemTypeRepository = itemTypeRepository ?? throw new ArgumentNullException(nameof(itemTypeRepository));
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




        public async Task CreateItemAsync(CreateItemDTO itemDTO)
        {
            if (itemDTO == null)
                throw new ArgumentNullException(nameof(itemDTO));

            // Map CreateItemDTO to the Item entity
            var item = new Item
            {
                Name = itemDTO.Name,
                Description = itemDTO.Description,
                Quantity = itemDTO.Quantity,
                Price = itemDTO.Price,
                CreatedById = itemDTO.CreatedById ?? 1, // Assuming you handle CreatedById logic elsewhere
                ItemTypeId = itemDTO.ItemTypeId
            };

            // Call repository to create item
            await _itemRepository.CreateItemAsync(item);

            // Save changes in the repository
            await _itemRepository.SaveChangesAsync();
        }


        public async Task UpdateItemAsync(int itemId, UpdateItemDTO itemDTO)
        {
            // Fetch the existing item by ID
            var item = await GetItemByIdAsync(itemId);
            if (item == null)
                throw new KeyNotFoundException($"Item with ID {itemId} not found.");

            // Validate that the ItemTypeId exists in the database
            var itemType = await _itemTypeRepository.GetItemTypeByIdAsync(itemDTO.ItemTypeId);
            if (itemType == null)
            {
                throw new KeyNotFoundException($"ItemType with ID {itemDTO.ItemTypeId} not found.");
            }

            // Update item properties based on DTO
            item.Name = itemDTO.Name;
            item.Description = itemDTO.Description;
            item.Quantity = itemDTO.Quantity;
            item.Price = itemDTO.Price;
            item.ItemTypeId = itemDTO.ItemTypeId;
            item.ItemType = itemType;  // Explicitly set the ItemType entity

            // Update the item in the repository
            await _itemRepository.UpdateItemAsync(item);  // Ensure Update is correctly implemented

            // Save changes
            await _itemRepository.SaveChangesAsync(); // This finalizes the update operation
        }






        public async Task DeleteItemAsync(int itemId)
        {
            await _itemRepository.DeleteItemAsync(itemId);
        }
    }
}
