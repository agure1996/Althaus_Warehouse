using Althaus_Warehouse.Models.Entities;

namespace Althaus_Warehouse.Services.Repositories
{
    /// <summary>
    /// Interface for managing item data in the warehouse.
    /// </summary>
    public interface IItemRepository
    {
        /// <summary>
        /// Retrieves all items in the warehouse.
        /// </summary>
        /// <returns>A list of <see cref="Item"/> objects.</returns>
        Task<IEnumerable<Item>> GetAllItemsAsync();

        /// <summary>
        /// Retrieves a specific item by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the item.</param>
        /// <returns>The <see cref="Item"/> object if found; otherwise, null.</returns>
        Task<Item> GetItemByIdAsync(int id);

        /// <summary>
        /// Adds a new item to the warehouse.
        /// </summary>
        /// <param name="item">The <see cref="Item"/> object to add.</param>
        Task AddItemAsync(Item item);

        /// <summary>
        /// Updates an existing item in the warehouse.
        /// </summary>
        /// <param name="item">The <see cref="Item"/> object with updated values.</param>
        Task UpdateItemAsync(Item item);

        /// <summary>
        /// Deletes an item from the warehouse by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the item to delete.</param>
        Task DeleteItemAsync(int id);

        /// <summary>
        /// Checks if an item exists by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the item to check.</param>
        /// <returns>True if the item exists; otherwise, false.</returns>
        Task<bool> ItemExistsAsync(int id);

        /// <summary>
        /// Retrieves items by their name.
        /// </summary>
        /// <param name="name">The name of the item.</param>
        /// <returns>A list of matching <see cref="Item"/> objects.</returns>
        Task<IEnumerable<Item>> GetItemsByNameAsync(string name);

        /// <summary>
        /// Saves any changes made in the repository to the database.
        /// </summary>
        /// <returns>True if the changes were saved successfully; otherwise, false.</returns>
        Task<bool> SaveChangesAsync();
        Task<IEnumerable<Item>> GetItemsByCategoryAsync(ItemType value);
    }
}
