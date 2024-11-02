using Althaus_Warehouse.Models.DTO.ItemDTOs;
using Althaus_Warehouse.Models.Entities;
using Althaus_Warehouse.Services.ItemService;
using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Althaus_Warehouse.Controllers
{
    [Route("api/v{version:apiVersion}/items")]
    [ApiVersion("1.0")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly ILogger<ItemController> _logger;
        private readonly IItemService _itemService;
        private readonly IMapper _mapper;

        public ItemController(ILogger<ItemController> logger, IItemService itemService, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _itemService = itemService ?? throw new ArgumentNullException(nameof(itemService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // GET: api/v1/items
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<(IEnumerable<GetItemDTO> Items, int TotalCount)>> GetAllItems(int pageSize = 5, int currentPage = 1)
        {
            if (pageSize <= 0 || currentPage <= 0)
                return BadRequest("Page size and current page must be greater than zero.");

            try
            {
                var (items, totalCount) = await _itemService.GetAllItemsAsync(pageSize, currentPage);
                var itemDTOs = _mapper.Map<IEnumerable<GetItemDTO>>(items);

                return Ok(new { Items = itemDTOs, TotalCount = totalCount });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving items.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        // GET: api/v1/items/{itemId}
        [HttpGet("{itemId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetItemDTO>> GetItemById(int itemId)
        {
            try
            {
                var item = await _itemService.GetItemByIdAsync(itemId);
                if (item == null)
                    return NotFound($"Item with ID {itemId} was not found.");

                var itemDTO = _mapper.Map<GetItemDTO>(item);
                return Ok(itemDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the item by ID.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateItemDTO itemDTO)
        {
            if (itemDTO == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return bad request with validation errors
            }

            try
            {
                // Map CreateItemDTO to Item entity and call the service
                var createdItem = await _itemService.CreateItemAsync(itemDTO);
                return CreatedAtAction(nameof(GetItemById), new { itemId = createdItem.Id }, createdItem); // Return 201 with location header
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the item.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }


        [HttpPut("{itemId}")]
        public async Task<IActionResult> UpdateItem(int itemId, [FromBody] UpdateItemDTO updateItemDTO)
        {
            if (updateItemDTO == null)
                return BadRequest("Item data cannot be null.");

            try
            {
                // Perform update operation
                await _itemService.UpdateItemAsync(itemId, updateItemDTO);

                return NoContent(); // Or return appropriate response
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Item with ID {itemId} not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the item.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }


        // DELETE: api/v1/items/{itemId}
        [HttpDelete("{itemId:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteItem(int itemId)
        {
            try
            {
                var item = await _itemService.GetItemByIdAsync(itemId);
                if (item == null)
                    return NotFound($"Item with ID {itemId} was not found.");

                await _itemService.DeleteItemAsync(itemId);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the item.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}
