using Althaus_Warehouse.Models.DTO.ItemDTOs;
using Althaus_Warehouse.Models.Entities;
using Althaus_Warehouse.Services.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Althaus_Warehouse.Controllers
{
    /// <summary>
    /// Controller to manage operations related to items in the warehouse.
    /// </summary>
    [Route("api/v{version:apiVersion}/items")]
    [Authorize(Roles = "Manager")] // Restricting my endpoint access to Admin users
    [Asp.Versioning.ApiVersion(1.0)]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly ILogger<ItemController> _logger; 
        private readonly IItemRepository _itemRepository;
        private readonly IItemTypeRepository _itemTypeRepository;
        private readonly IMapper _mapper;

        public ItemController(ILogger<ItemController> logger, IItemRepository itemRepository, IMapper mapper, IItemTypeRepository itemTypeRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
            _itemTypeRepository = itemTypeRepository ?? throw new ArgumentNullException(nameof(itemTypeRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // Existing GetItems() method remains unchanged...

        /// <summary>
        /// Creates a new item in the warehouse.
        /// </summary>
        /// <param name="item">The item data to create as <see cref="CreateItemDTO"/>.</param>
        /// <returns>The created item as <see cref="GetItemDTO"/>.</returns>
        /// <response code="201">If the item was created successfully.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin")] // Ensure only Admins can create items
        public async Task<ActionResult<GetItemDTO>> CreateItem([FromBody] CreateItemDTO item)
        {
            if (item == null)
            {
                return BadRequest("Item data is null.");
            }

            try
            {
                var newItem = _mapper.Map<Item>(item);

                if (item.ItemTypeId > 0) // Assuming ItemTypeId must be a positive number
                {
                    var itemType = await _itemTypeRepository.GetItemTypeByIdAsync(item.ItemTypeId);
                    newItem.ItemType = itemType;
                }

                await _itemRepository.AddItemAsync(newItem);
                await _itemRepository.SaveChangesAsync();

                var createdItem = _mapper.Map<GetItemDTO>(newItem);
                createdItem.InStock = newItem.Quantity > 0;

                _logger.LogInformation("Item with ID {Id} created successfully.", createdItem.Id);
                return CreatedAtRoute("GetItem", new { id = createdItem.Id }, createdItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating a new item.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        // Additional methods like UpdateItem and DeleteItem should also have the [Authorize(Roles = "Admin")] attribute added.

        /// <summary>
        /// Updates an existing item in the warehouse.
        /// </summary>
        /// <param name="id">The ID of the item to update.</param>
        /// <param name="itemBeingUpdated">The updated item data as <see cref="UpdateItemDTO"/>.</param>
        /// <response code="204">If the item was updated successfully.</response>
        /// <response code="404">If the item is not found.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Admin")] // Ensure only Admins can update items
        public async Task<ActionResult> UpdateItem(int id, [FromBody] UpdateItemDTO itemBeingUpdated)
        {
            if (itemBeingUpdated == null)
            {
                return BadRequest("Item data is null.");
            }

            try
            {
                var itemEntity = await _itemRepository.GetItemByIdAsync(id);
                if (itemEntity == null)
                {
                    _logger.LogWarning("Item with ID {Id} not found for update.", id);
                    return NotFound($"Item with ID {id} was not found.");
                }

                itemEntity.Name = itemBeingUpdated.Name;
                itemEntity.Description = itemBeingUpdated.Description;
                itemEntity.Quantity = itemBeingUpdated.Quantity;
                itemEntity.Price = itemBeingUpdated.Price;
                itemEntity.ItemTypeId = itemBeingUpdated.ItemTypeId;

                await _itemRepository.SaveChangesAsync();

                _logger.LogInformation("Item with ID {Id} updated successfully.", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating item with ID {Id}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Deletes an item from the warehouse.
        /// </summary>
        /// <param name="id">The ID of the item to delete.</param>
        /// <response code="204">If the item was deleted successfully.</response>
        /// <response code="404">If the item is not found.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "Admin")] // Ensure only Admins can delete items
        public async Task<ActionResult> DeleteItem(int id)
        {
            try
            {
                var itemEntity = await _itemRepository.GetItemByIdAsync(id);
                if (itemEntity == null)
                {
                    _logger.LogWarning("Item with ID {Id} not found for deletion.", id);
                    return NotFound($"Item with ID {id} was not found.");
                }

                await _itemRepository.DeleteItemAsync(id);
                await _itemRepository.SaveChangesAsync();

                _logger.LogInformation("Item with ID {Id} deleted successfully.", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting item with ID {Id}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}
