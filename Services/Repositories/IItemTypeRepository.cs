using Althaus_Warehouse.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Althaus_Warehouse.Services.Repositories
{
    /// <summary>
    /// Defines the contract for item type repository operations.
    /// </summary>
    public interface IItemTypeRepository
    {
        /// <summary>
        /// Retrieves all item types.
        /// </summary>
        /// <returns>A list of all item types as <see cref="ItemType"/>.</returns>
        Task<IEnumerable<ItemType>> GetAllItemTypesAsync();

        /// <summary>
        /// Retrieves a specific item type by ID.
        /// </summary>
        /// <param name="id">The ID of the item type to retrieve.</param>
        /// <returns>The requested item type as <see cref="ItemType"/>.</returns>
        Task<ItemType> GetItemTypeByIdAsync(int id);
        /// <summary>
        /// Retrieves item by id.
        /// </summary>
        /// <returns>An item <see cref="Item"/>.</returns>
        Task<Item> GetItemByIdAsync(int id);

        /// <summary>
        /// Retrieves a specific item type by name.
        /// </summary>
        /// <param name="name">The name of the item type to retrieve.</param>
        /// <returns>The requested item type as <see cref="ItemType"/>.</returns>
        Task<ItemType> GetItemTypeByNameAsync(string name);

        /// <summary>
        /// check if item type exists.
        /// </summary>
        /// <param name="itemTypeId">The id of the item type to retrieve.</param>
        /// <returns>Returns true.</returns>
        Task<bool> ItemTypeExistsAsync(int itemTypeId);

        /// <summary>
        /// Adds a new item type.
        /// </summary>
        /// <param name="itemType">The item type to add as <see cref="ItemType"/>.</param>
        Task AddItemTypeAsync(ItemType itemType);

        /// <summary>
        /// Updates an existing item type.
        /// </summary>
        /// <param name="itemType">The updated item type as <see cref="ItemType"/>.</param>
        Task UpdateItemTypeAsync(ItemType itemType);

        /// <summary>
        /// Deletes an item type by ID.
        /// </summary>
        /// <param name="id">The ID of the item type to delete.</param>
        Task DeleteItemTypeAsync(int id);

        /// <summary>
        /// Saves changes to the database.
        /// </summary>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task SaveChangesAsync();
    }
}
