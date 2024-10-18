using Althaus_Warehouse.Models.DTO.ItemDTOs;
using Althaus_Warehouse.Models.Entities;
using Althaus_Warehouse.Services.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace MyWarehouse.API.Controllers
{
    /// <summary>
    /// Controller to manage operations related to items in the warehouse.
    /// </summary>
    [Route("api/v{version:apiVersion}/items")]
    [Asp.Versioning.ApiVersion(1.0)]
    [ApiController]
    //[Authorize]
    public class ItemsController : ControllerBase
    {
        private readonly ILogger<ItemsController> _logger;
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;

        public ItemsController(ILogger<ItemsController> logger, IItemRepository itemRepository, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
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
            var items = await _itemRepository.GetAllItemsAsync();
            return Ok(_mapper.Map<IEnumerable<GetItemDTO>>(items));
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
            var item = await _itemRepository.GetItemByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<GetItemDTO>(item));
        }

        /// <summary>
        /// Creates a new item in the warehouse.
        /// </summary>
        /// <param name="item">The item data to create as <see cref="CreateItemDTO"/>.</param>
        /// <returns>The created item as <see cref="GetItemDTO"/>.</returns>
        /// <response code="201">If the item was created successfully.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<GetItemDTO>> CreateItem([FromBody] CreateItemDTO item)
        {
            var newItem = _mapper.Map<Item>(item);
            await _itemRepository.AddItemAsync(newItem);
            await _itemRepository.SaveChangesAsync();

            var createdItem = _mapper.Map<GetItemDTO>(newItem);

            return CreatedAtRoute("GetItem", new { id = createdItem.Id }, createdItem);
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
        public async Task<ActionResult> UpdateItem(int id, [FromBody] UpdateItemDTO itemBeingUpdated)
        {
            var itemEntity = await _itemRepository.GetItemByIdAsync(id);
            if (itemEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(itemBeingUpdated, itemEntity);
            await _itemRepository.SaveChangesAsync();

            return NoContent();
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
            var itemEntity = await _itemRepository.GetItemByIdAsync(id);
            if (itemEntity == null)
            {
                return NotFound();
            }

            var itemToPatch = _mapper.Map<UpdateItemDTO>(itemEntity);
            patchDocument.ApplyTo(itemToPatch, ModelState);

            if (!ModelState.IsValid || !TryValidateModel(itemToPatch))
            {
                return BadRequest(ModelState);
            }

            _mapper.Map(itemToPatch, itemEntity);
            await _itemRepository.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Retrieves a list of items based on the specified category type.
        /// </summary>
        /// <param name="itemType">The category of items to retrieve. This should be a valid <see cref="ItemType"/> enum value.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains an <see cref="ActionResult{IEnumerable{GetItemDTO}}"/> 
        /// which can be:
        /// <list type="bullet">
        ///     <item>
        ///         <description>A 200 OK response containing a list of <see cref="GetItemDTO"/> if items are found.</description>
        ///     </item>
        ///     <item>
        ///         <description>A 400 Bad Request response if the provided item type is invalid.</description>
        ///     </item>
        ///     <item>
        ///         <description>A 404 Not Found response if no items are found for the specified category.</description>
        ///     </item>
        /// </list>
        /// </returns>
        /// <remarks>
        /// Example request: GET /api/items/category?itemType=Electronics
        /// </remarks>
        [HttpGet("category")]
        public async Task<ActionResult<IEnumerable<GetItemDTO>>> GetItemsByCategory([FromQuery] ItemType itemType)
        {
            // Validate that the itemType is a valid enum value
            if (!Enum.IsDefined(typeof(ItemType), itemType))
            {
                return BadRequest("Invalid item type provided.");
            }

            // Retrieve the items by category
            var items = await _itemRepository.GetItemsByCategoryAsync(itemType);

            // Check if any items were found
            if (items == null || !items.Any())
            {
                return NotFound($"No items found for the category: {itemType}");
            }

            // Map the items to DTOs
            var itemDTOs = _mapper.Map<IEnumerable<GetItemDTO>>(items);

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
            var itemEntity = await _itemRepository.GetItemByIdAsync(id);
            if (itemEntity == null)
            {
                return NotFound();
            }

            await _itemRepository.DeleteItemAsync(itemEntity.Id);
            await _itemRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}
