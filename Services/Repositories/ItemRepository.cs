﻿using Althaus_Warehouse.DBContext;
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
        public ItemRepository(WarehouseDbContext context) => _context = context ?? throw new ArgumentNullException(nameof(context));

        /// <inheritdoc/>
        public async Task<IEnumerable<Item>> GetAllItemsAsync() =>
            // Retrieve all items from the database
            await _context.Items.ToListAsync();
        public async Task<IEnumerable<Item>> GetItemsByCategoryAsync(ItemType value) =>
            await _context.Items
                .Where(item => item.ItemType == value) // Filter items by the specified ItemType
                .ToListAsync(); // Execute the query and return the results as a list


        /// <inheritdoc/>
        public async Task<Item> GetItemByIdAsync(int id) =>
            // Retrieve a specific item by its ID
            await _context.Items.FindAsync(id);

        /// <inheritdoc/>
        public async Task AddItemAsync(Item item)
        {
            // Add a new item to the database
            await _context.Items.AddAsync(item);
        }

        /// <inheritdoc/>
        public async Task UpdateItemAsync(Item item)
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
            // Retrieve items by name (case-insensitive)
            await _context.Items
                .Where(i => EF.Functions.Like(i.Name, $"%{name}%"))
                .ToListAsync();

        /// <inheritdoc/>
        public async Task<bool> SaveChangesAsync() =>
            // Save changes to the database
            (await _context.SaveChangesAsync()) > 0;
    }
}
