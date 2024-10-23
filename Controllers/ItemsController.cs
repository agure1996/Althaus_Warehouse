using Microsoft.AspNetCore.Mvc;
using Althaus_Warehouse.Models.DTO.ItemDTOs;
using Althaus_Warehouse.Models.Entities;
using Althaus_Warehouse.Models;
using Althaus_Warehouse.Services.ItemService;
using Althaus_Warehouse.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Althaus_Warehouse.Controllers
{
    public class ItemsController : Controller
    {
        private readonly IItemService _itemService;

        public ItemsController(IItemService itemService)
        {
            _itemService = itemService ?? throw new ArgumentNullException(nameof(itemService));
        }

        public async Task<IActionResult> Index(int pageSize = 4, int currentPage = 1)
        {
            var result = await _itemService.GetAllItemsAsync(pageSize, currentPage);
            var items = result.Items; // Assuming this returns an IEnumerable<Item>
            var totalCount = result.TotalCount; // Assuming this returns the total item count

            ViewBag.PaginationMetaData = new PaginationMetaData(totalCount, pageSize, currentPage);

            return View(items);
        }



        [HttpGet]
        public IActionResult SearchItemById()
        {
            return View(); // This renders the search form to input employee ID
        }

        [HttpGet]
        public async Task<IActionResult> GetItemById(int id)
        {
            var item = await _itemService.GetItemByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item); // Change this line to return JSON
        }



        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateItemDTO itemDTO)
        {
            if (ModelState.IsValid)
            {
                // Map CreateItemDTO to Item entity
                var item = new Item
                {
                    Name = itemDTO.Name,
                    Description = itemDTO.Description,
                    Quantity = itemDTO.Quantity,
                    Price = itemDTO.Price,
                    CreatedById = itemDTO.CreatedById, // This can be set based on your application's logic
                    ItemTypeId = itemDTO.ItemTypeId
                };

                await _itemService.CreateItemAsync(item);
                return RedirectToAction(nameof(Index));
            }

            return View(itemDTO);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var item = await _itemService.GetItemByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _itemService.DeleteItemAsync(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View("Error", new ErrorViewModel { RequestId = ex.Message });
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            var item = await _itemService.GetItemByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }



        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var item = await _itemService.GetItemByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            // Retrieve all item types
            var itemTypes = await _itemService.GetAllItemTypesAsync();
            if (itemTypes == null || !itemTypes.Any())
            {
                Console.WriteLine("No item types found");
            }

            var itemDTO = new UpdateItemDTO
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Quantity = item.Quantity,
                Price = Math.Round(item.Price, 2),
                ItemTypeId = item.ItemTypeId, // Current item's type
            };

            ViewBag.ItemTypes = new SelectList(itemTypes, "Id", "Name", item.ItemTypeId); // Prepare item types for view

            return View(itemDTO);
        }




        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateItemDTO itemDTO)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }

            try
            {
                // Round the price to two decimal places
                itemDTO.Price = Math.Round(itemDTO.Price, 2);

                await _itemService.UpdateItemAsync(id, itemDTO);
                return RedirectToAction("Details", "Items", new { id = id });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error. Please try again later.");
            }
        }










    }
}
