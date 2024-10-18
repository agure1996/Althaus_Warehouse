using Althaus_Warehouse.DBContext;
using Althaus_Warehouse.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Althaus_Warehouse.Services.Repositories
{
    /// <summary>
    /// Repository class for managing item data in the warehouse.
    /// Implements <see cref="IItemRepository"/>.
    /// </summary>
    public class ItemRepository : IItemRepository
    {
        private readonly WarehouseDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemRepository"/> class.
        /// </summary>
        /// <param name="context">The database context to use.</param>
        public ItemRepository(WarehouseDbContext context) => _context = context;

        /// <inheritdoc/>
        public async Task<IEnumerable<Item>> GetAllItemsAsync() =>
             // Retrieve all items from the database
             await _context.Items.ToListAsync();

        /// <inheritdoc/>
        public async Task<Item> GetItemByIdAsync(int id) =>
             // Retrieve a specific item by its ID
             await _context.Items.FindAsync(id);


        /// <inheritdoc/>
        public async Task AddItemAsync(Item item)
        {
            // Add a new item to the database
            await _context.Items.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task UpdateItemAsync(Item item)
        {
            // Update an existing item in the database
            _context.Items.Update(item);
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task DeleteItemAsync(int id)
        {
            // Find and delete an item by its ID
            var item = await GetItemByIdAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
            }
        }
    }
}