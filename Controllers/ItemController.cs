using Althaus_Warehouse.Models.DTO.ItemDTOs;
using Althaus_Warehouse.Models.Entities;
using Althaus_Warehouse.Services.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MyWarehouse.API.Controllers
{
    /// <summary>
    /// Controller to manage operations related to items in the warehouse.
    /// </summary>
    [Route("api/v{version:apiVersion}/items")]
    [Asp.Versioning.ApiVersion(1.0)]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly ILogger _logger;
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

        /// <summary>
        /// Retrieves a list of all items in the warehouse.
        /// </summary>
        /// <returns>A list of items as <see cref="GetItemDTO"/>.</returns>
        /// <response code="200">Returns the list of items.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GetItemDTO>>> GetItems()
        {
            try
            {
                var items = await _itemRepository.GetAllItemsAsync();
                var itemDtos = _mapper.Map<IEnumerable<GetItemDTO>>(items);

                foreach (var item in itemDtos)
                {
                    item.InStock = item.Quantity > 0;
                }

                return Ok(itemDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all items.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Retrieves a specific item by ID.
        /// </summary>
        /// <param name="id">The ID of the item to retrieve.</param>
        /// <returns>The requested item as <see cref="GetItemDTO"/>.</returns>
        /// <response code="200">Returns the requested item.</response>
        /// <response code="404">If the item is not found.</response>
        [HttpGet("{id}", Name = "GetItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetItemDTO>> GetItem(int id)
        {
            try
            {
                var item = await _itemRepository.GetItemByIdAsync(id);
                if (item == null)
                {
                    _logger.LogWarning("Item with ID {Id} not found.", id);
                    return NotFound($"Item with ID {id} was not found.");
                }

                var itemDto = _mapper.Map<GetItemDTO>(item);
                itemDto.InStock = item.Quantity > 0;

                return Ok(itemDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving item with ID {Id}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Creates a new item in the warehouse.
        /// </summary>
        /// <param name="item">The item data to create as <see cref="CreateItemDTO"/>.</param>
        /// <returns>The created item as <see cref="GetItemDTO"/>.</returns>
        /// <response code="201">If the item was created successfully.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetItemDTO>> CreateItem([FromBody] CreateItemDTO item)
        {
            if (item == null)
            {
                return BadRequest("Item data is null.");
            }

            try
            {
                var newItem = _mapper.Map<Item>(item);
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

                _mapper.Map(itemBeingUpdated, itemEntity);
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
        /// Applies a partial update to an existing item in the warehouse.
        /// </summary>
        /// <param name="id">The ID of the item to update.</param>
        /// <param name="patchDocument">The JSON Patch document with the update instructions as <see cref="JsonPatchDocument"/>.</param>
        /// <response code="204">If the item was updated successfully.</response>
        /// <response code="400">If the patch document is invalid.</response>
        /// <response code="404">If the item is not found.</response>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> PartiallyUpdateItem(int id, [FromBody] JsonPatchDocument<UpdateItemDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest("Patch document is null.");
            }

            try
            {
                var itemEntity = await _itemRepository.GetItemByIdAsync(id);
                if (itemEntity == null)
                {
                    _logger.LogWarning("Item with ID {Id} not found for partial update.", id);
                    return NotFound($"Item with ID {id} was not found.");
                }

                var itemToPatch = _mapper.Map<UpdateItemDTO>(itemEntity);
                patchDocument.ApplyTo(itemToPatch, ModelState);

                if (!ModelState.IsValid || !TryValidateModel(itemToPatch))
                {
                    return BadRequest(ModelState);
                }

                _mapper.Map(itemToPatch, itemEntity);
                await _itemRepository.SaveChangesAsync();

                _logger.LogInformation("Item with ID {Id} partially updated successfully.", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error partially updating item with ID {Id}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Retrieves items by their category.
        /// Can retrieve items by item type ID or category name.
        /// </summary>
        /// <param name="itemType">The ID of the item type/category.</param>
        /// <param name="categoryName">The name of the category.</param>
        /// <returns>A list of items as <see cref="GetItemDTO"/>.</returns>
        /// <response code="200">Returns the list of items for the specified category.</response>
        /// <response code="400">If the item type ID is invalid or both parameters are missing.</response>
        /// <response code="404">If no items are found for the category.</response>
        [HttpGet("category")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<GetItemDTO>>> GetItemsByCategory([FromQuery] int? itemTypeId = null, [FromQuery] string categoryName = null)
        {
            // Check if at least one parameter is provided
            if (!itemTypeId.HasValue && string.IsNullOrWhiteSpace(categoryName))
            {
                return BadRequest("Please provide either an itemTypeId or a categoryName.");
            }

            // Log the incoming parameters
            _logger.LogInformation("Received request for GetItemsByCategory with itemTypeId: {itemTypeId}, categoryName: {categoryName}", itemTypeId, categoryName);

            // Retrieve items using the repository method
            var items = await _itemRepository.GetItemsByCategoryAsync(itemTypeId, categoryName);

            // Check if any items were found
            if (items == null || !items.Any())
            {
                return NotFound("No items found for the provided criteria.");
            }

            // Map the items to DTOs
            var itemDTOs = _mapper.Map<IEnumerable<GetItemDTO>>(items);
            foreach (var item in itemDTOs)
            {
                item.InStock = item.Quantity > 0; // Assuming Quantity is a property of Item
            }

            return Ok(itemDTOs);
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
