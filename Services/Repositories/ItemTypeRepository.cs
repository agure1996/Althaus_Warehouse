using Althaus_Warehouse.DBContext;
using Althaus_Warehouse.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Althaus_Warehouse.Services.Repositories
{
    /// <summary>
    /// Repository class for managing item types in the warehouse.
    /// Implements <see cref="IItemTypeRepository"/>.
    /// </summary>
    public class ItemTypeRepository : IItemTypeRepository
    {
#pragma warning disable CS8603 // Possible null reference return.
        private readonly WarehouseDbContext _context;
        private readonly ILogger<ItemTypeRepository> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemTypeRepository"/> class.
        /// </summary>
        /// <param name="context">The database context to use.</param>
        /// <param name="logger">The logger to use for logging operations.</param>
        public ItemTypeRepository(WarehouseDbContext context, ILogger<ItemTypeRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Retrieves an item type by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the item type.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. 
        /// The task result contains the item type with the specified ID, 
        /// or null if not found.
        /// </returns>
        public async Task<ItemType> GetItemTypeByIdAsync(int id)
        {
            // Find the item type by ID
            var itemType = await _context.ItemTypes.FindAsync(id);
            if (itemType == null)
            {
                _logger.LogWarning($"Item type with ID {id} not found.");
            }
            return itemType; // Return null if not found
        }

        /// <summary>
        /// Retrieves all item types.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation, containing a list of item types.</returns>
        public async Task<IEnumerable<ItemType>> GetAllItemTypesAsync()
        {
            return await _context.ItemTypes.ToListAsync();
        }

        /// <summary>
        /// Retrieves a specific item type by name.
        /// </summary>
        /// <param name="name">The name of the item type to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation, containing the item type or null if not found.</returns>
        public async Task<ItemType> GetItemTypeByNameAsync(string name)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            return await _context.ItemTypes.FirstOrDefaultAsync(it => it.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        /// <summary>
        /// Adds a new item type.
        /// </summary>
        /// <param name="itemType">The item type to add.</param>
        public async Task AddItemTypeAsync(ItemType itemType)
        {
            await _context.ItemTypes.AddAsync(itemType);
            await SaveChangesAsync();
        }

        /// <summary>
        /// Updates an existing item type.
        /// </summary>
        /// <param name="itemType">The updated item type.</param>
        public async Task UpdateItemTypeAsync(ItemType itemType)
        {
            _context.ItemTypes.Update(itemType);
            await SaveChangesAsync();
        }

        /// <summary>
        /// Deletes an item type by ID.
        /// </summary>
        /// <param name="id">The ID of the item type to delete.</param>
        public async Task DeleteItemTypeAsync(int id)
        {
            var itemType = await GetItemTypeByIdAsync(id);
            if (itemType != null)
            {
                _context.ItemTypes.Remove(itemType);
                await SaveChangesAsync();
            }
            else
            {
                _logger.LogWarning($"Attempted to delete item type with ID {id}, but it was not found.");
            }
        }

        /// <summary>
        /// Saves changes to the database.
        /// </summary>
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
