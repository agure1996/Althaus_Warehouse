using Althaus_Warehouse.Models.DTO.ItemDTOs;
using Althaus_Warehouse.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Althaus_Warehouse.Services.ItemService
{
    public interface IItemService
    {
        Task<Item?> GetItemByIdAsync(int itemId);
        Task<IEnumerable<Item>> GetAllItemsAsync();
        Task CreateItemAsync(Item item);
        Task UpdateItemAsync(int itemId, UpdateItemDTO itemDTO);
        Task DeleteItemAsync(int itemId);
    }


}
