using Althaus_Warehouse.DBContext;
using Althaus_Warehouse.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Althaus_Warehouse.Services.Repositories
{
#pragma warning disable CS8603 // Possible null reference return.
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
        public ItemRepository(WarehouseDbContext context) =>
            _context = context ?? throw new ArgumentNullException(nameof(context));


        /// <inheritdoc/>
        public async Task<(IEnumerable<Item> Items, int TotalCount)> GetAllItemsAsync(int pageSize, int currentPage)
        {
            var totalCount = await _context.Items.CountAsync();
            var items = await _context.Items
                .Include(item => item.ItemType) // Ensure the ItemType is included
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (items, totalCount);
        }








        /// <inheritdoc/>
        public async Task<IEnumerable<Item>> GetItemsByCategoryAsync(int itemTypeId)
        {
            var query = _context.Items
                .Where(i => i.ItemTypeId == itemTypeId)
                .Include(i => i.ItemType); // Include related ItemType

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Item>> GetItemsByCategoryNameAsync(string categoryName)
        {
            if (string.IsNullOrWhiteSpace(categoryName))
            {
                throw new ArgumentException("Category name cannot be null or empty.", nameof(categoryName));
            }

            var query = _context.Items
                .Include(i => i.ItemType) // Include related ItemType
                .Where(item => item.ItemType.Name.ToLower() == categoryName.ToLower()); // Case-insensitive comparison

            return await query.ToListAsync();
        }



        /// <inheritdoc/>
        public async Task<Item> GetItemByIdAsync(int id)
        {

            return await _context.Items
                .Include(i => i.ItemType)  // Ensure ItemType is included in the query
                .FirstOrDefaultAsync(i => i.Id == id);

        }

        /// <inheritdoc/>
        public async Task AddItemAsync(Item item)
        {
            // Ensure DateCreated is set when adding the item
            item.DateCreated = DateTime.UtcNow;
            await _context.Items.AddAsync(item);
        }

        /// <inheritdoc/>
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task UpdateItemAsync(Item item)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            // Update an existing item in the database
            _context.Items.Update(item);
        }

        /// <inheritdoc/>
        public async Task DeleteItemAsync(int id)
        {
            // Find and delete an item by its ID
            var item = await GetItemByIdAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
            }
        }

        /// <inheritdoc/>
        public async Task<bool> ItemExistsAsync(int id) =>
            // Check if an item with the specified ID exists
            await _context.Items.AnyAsync(i => i.Id == id);

        /// <inheritdoc/>
        public async Task<IEnumerable<Item>> GetItemsByNameAsync(string name) =>
            // Retrieve items by name (case-insensitive), include related ItemType
            await _context.Items
                .Include(i => i.ItemType)  // Ensure ItemType is included in the query
                .Where(i => EF.Functions.Like(i.Name, $"%{name}%"))
                .ToListAsync();

        /// <inheritdoc/>
        public async Task<bool> SaveChangesAsync() =>
            // Save changes to the database
            (await _context.SaveChangesAsync()) > 0;

        public async Task<IEnumerable<Item>> GetItemsByCategoryIdAsync(int? itemTypeId = null)
        {
            var query = _context.Items.AsQueryable();

            // If itemTypeId is provided, filter items by that ID
            if (itemTypeId.HasValue)
            {
                query = query.Where(i => i.ItemTypeId == itemTypeId.Value);
            }

            // Include related ItemType
            query = query.Include(i => i.ItemType);

            return await query.ToListAsync();
        }

    }
}
