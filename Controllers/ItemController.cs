using Althaus_Warehouse.Models.DTO.ItemDTOs;
using Althaus_Warehouse.Models.Entities;
using Althaus_Warehouse.Services.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Althaus_Warehouse.Controllers
{
    [Route("api/v{version:apiVersion}/items")]
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

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GetItemDTO>>> GetAllItems()
        {
            try
            {
                var items = await _itemRepository.GetAllItemsAsync();
                var itemDTOs = _mapper.Map<IEnumerable<GetItemDTO>>(items);
                return Ok(itemDTOs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all items.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet("{id}", Name = "GetItemById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetItemDTO>> GetItemById(int id)
        {
            try
            {
                var itemEntity = await _itemRepository.GetItemByIdAsync(id);
                if (itemEntity == null)
                {
                    _logger.LogWarning("Item with ID {Id} not found.", id);
                    return NotFound($"Item with ID {id} was not found.");
                }

                var itemDTO = _mapper.Map<GetItemDTO>(itemEntity);
                return Ok(itemDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving item with ID {Id}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Manager")]
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
                if (item.ItemTypeId > 0)
                {
                    var itemType = await _itemTypeRepository.GetItemTypeByIdAsync(item.ItemTypeId);
                    newItem.ItemType = itemType;
                }

                await _itemRepository.AddItemAsync(newItem);
                await _itemRepository.SaveChangesAsync();

                var createdItem = _mapper.Map<GetItemDTO>(newItem);
                createdItem.InStock = newItem.Quantity > 0;

                _logger.LogInformation("Item with ID {Id} created successfully.", createdItem.Id);
                return CreatedAtRoute("GetItemById", new { id = createdItem.Id }, createdItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating a new item.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        // Add Authorization attribute for Admin
        [HttpPut("{id}")]
        [Authorize(Roles = "Manager")]
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

        [HttpDelete("{id}")]
        [Authorize(Roles = "Manager")]
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

        [HttpGet("category")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<GetItemDTO>>> GetItemsByItemTypeId([FromQuery] int itemTypeId)
        {
            try
            {
                var items = await _itemRepository.GetItemsByCategoryIdAsync(itemTypeId);
                if (items == null || !items.Any())
                {
                    _logger.LogWarning("No items found for Item Type ID {ItemTypeId}.", itemTypeId);
                    return NotFound($"No items found for Item Type ID {itemTypeId}.");
                }

                var itemDTOs = _mapper.Map<IEnumerable<GetItemDTO>>(items);
                return Ok(itemDTOs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving items for Item Type ID {ItemTypeId}.", itemTypeId);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }


        // Change the route to avoid conflict
        [HttpGet("category/name")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<GetItemDTO>>> GetItemsByItemTypeName([FromQuery] string? categoryTypeName)
        {
            try
            {
                var items = await _itemRepository.GetItemsByCategoryNameAsync(categoryTypeName);
                if (items == null || !items.Any())
                {
                    _logger.LogWarning("No items found for Category Name {CategoryTypeName}.", categoryTypeName);
                    return NotFound("No items found for the given category criteria.");
                }

                var itemDTOs = _mapper.Map<IEnumerable<GetItemDTO>>(items);
                return Ok(itemDTOs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving items for Category Name {CategoryTypeName}.", categoryTypeName);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}
