using Microsoft.AspNetCore.Mvc;
using Althaus_Warehouse.Models.DTO.ItemDTOs;
using Althaus_Warehouse.Models.Entities;
using Althaus_Warehouse.Models;
using Althaus_Warehouse.Services.ItemService;
using Althaus_Warehouse.Services;

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
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _itemService.GetItemByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            var itemDTO = new UpdateItemDTO
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Quantity = item.Quantity,
                Price = item.Price,
                ItemTypeId = item.ItemTypeId
            };

            return View(itemDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateItemDTO itemDTO)
        {
            if (ModelState.IsValid)
            {
                await _itemService.UpdateItemAsync(itemDTO.Id, itemDTO); // Ensure both parameters are passed
                return RedirectToAction("Index");
            }

            return View(itemDTO);
        }
    }
}
