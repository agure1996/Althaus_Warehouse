using Althaus_Warehouse.DBContext;
using Althaus_Warehouse.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Althaus_Warehouse.Services.Repositories
{
    public class ItemTypeRepository : IItemTypeRepository
    {
        private readonly WarehouseDbContext _context;
        private readonly ILogger<ItemTypeRepository> _logger;

        public ItemTypeRepository(WarehouseDbContext context, ILogger<ItemTypeRepository> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public async Task<Item> GetItemByIdAsync(int id)
        {
            return await _context.Items
                .Include(i => i.ItemType)  // Include the related ItemType
                .FirstOrDefaultAsync(i => i.Id == id);
        }


        /// <inheritdoc/>
        public async Task<IEnumerable<ItemType>> GetAllItemTypesAsync()
        {
            return await _context.ItemTypes.ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<ItemType?> GetItemTypeByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return null;

            // Convert both the name in the database and the input name to lowercase for comparison
            return await _context.ItemTypes
                .FirstOrDefaultAsync(it => it.Name.ToLower() == name.ToLower());
        }

        /// <inheritdoc/>
        public async Task<bool> ItemTypeExistsAsync(int itemTypeId)
        {
            return await _context.ItemTypes.AnyAsync(it => it.Id == itemTypeId);
        }

        /// <inheritdoc/>
        public async Task AddItemTypeAsync(ItemType itemType)
        {
            await _context.ItemTypes.AddAsync(itemType);
            await SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task UpdateItemTypeAsync(ItemType itemType)
        {
            var existingItemType = await GetItemByIdAsync(itemType.Id);
            if (existingItemType == null)
            {
                _logger.LogError($"Cannot update ItemType. ItemType with ID {itemType.Id} not found.");
                throw new KeyNotFoundException($"ItemType with ID {itemType.Id} not found.");
            }

            existingItemType.Name = itemType.Name;
            _context.Entry(existingItemType).State = EntityState.Modified;
            await SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task DeleteItemTypeAsync(int id)
        {
            var itemType = await GetItemTypeByIdAsync(id);
            if (itemType == null)
            {
                _logger.LogWarning($"Attempted to delete ItemType with ID {id}, but it was not found.");
                throw new KeyNotFoundException($"ItemType with ID {id} not found.");
            }

            _context.ItemTypes.Remove(itemType);
            await SaveChangesAsync();
        }

         /// <inheritdoc/>
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<ItemType?> GetItemTypeByIdAsync(int id)
        {
            // Query the context to find the ItemType by its ID
            var itemType = await _context.ItemTypes.FindAsync(id);

            // Optionally log if the item type was not found
            if (itemType == null)
            {
                _logger.LogWarning($"ItemType with ID {id} was not found.");
            }

            return itemType;
        }

    }
}
