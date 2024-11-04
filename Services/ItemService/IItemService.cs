using Althaus_Warehouse.Models.DTO.ItemDTOs;
using Althaus_Warehouse.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Althaus_Warehouse.Services.ItemService
{
    public interface IItemService
    {
        Task<Item?> GetItemByIdAsync(int itemId);
        Task<(IEnumerable<Item> Items, int TotalCount)> GetAllItemsAsync(int pageSize, int currentPage);
        Task<List<ItemType>> GetAllItemTypesAsync();
        Task<Item> CreateItemAsync(CreateItemDTO itemDTO);
        Task UpdateItemAsync(int itemId, UpdateItemDTO itemDTO);
        Task DeleteItemAsync(int itemId);

        Task<IEnumerable<Item>> GetItemsByCategoryTypeNameAsync(string?  categoryTypeName);
        Task<IEnumerable<Item>> GetItemsByItemTypeIdAsync(int itemTypeId);
    }


}
