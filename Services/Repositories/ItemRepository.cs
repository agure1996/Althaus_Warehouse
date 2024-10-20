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
        public async Task<IEnumerable<Item>> GetAllItemsAsync() =>
            // Retrieve all items from the database, including ItemType
            await _context.Items
                .Include(i => i.ItemType) // Ensure related ItemType is included
                .ToListAsync();

        /// <inheritdoc/>
#pragma warning disable CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).
        public async Task<IEnumerable<Item>> GetItemsByCategoryAsync(int? itemTypeId, string categoryName)
#pragma warning restore CS8767 // Nullability of reference types in type of parameter doesn't match implicitly implemented member (possibly because of nullability attributes).
        {
            var query = _context.Items.AsQueryable();

            // Filter by ItemTypeId if provided
            if (itemTypeId.HasValue)
            {
                query = query.Where(i => i.ItemTypeId == itemTypeId);
            }

            // Filter by categoryName (case-insensitive) if provided
            if (!string.IsNullOrWhiteSpace(categoryName))
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                query = query.Join(
                    _context.ItemTypes,
                    item => item.ItemTypeId,
                    itemType => itemType.Id,
                    (item, itemType) => new { Item = item, ItemType = itemType })
                    .Where(x => x.ItemType.Name.Equals(categoryName, StringComparison.CurrentCultureIgnoreCase)) // Case-insensitive comparison
                    .Select(x => x.Item);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            }

            // Include related ItemType
            query = query.Include(i => i.ItemType);

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
    }
}
