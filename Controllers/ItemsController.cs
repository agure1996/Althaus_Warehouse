﻿using Microsoft.AspNetCore.Mvc;
using Althaus_Warehouse.Models.DTO.ItemDTOs;
using Althaus_Warehouse.Models.Entities;
using Althaus_Warehouse.Models;
using Althaus_Warehouse.Services.ItemService;
using Althaus_Warehouse.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

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

        [Authorize(Policy = "RequireManager")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // Retrieve all item types to populate the dropdown
            var itemTypes = await _itemService.GetAllItemTypesAsync();
            ViewBag.ItemTypes = new SelectList(itemTypes, "Id", "Name"); // Prepare item types for view

            return View();
        }

        [Authorize(Policy = "RequireManager")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateItemDTO itemDTO)
        {
            if (ModelState.IsValid)
            {
                // Call the service method with the DTO directly
                await _itemService.CreateItemAsync(itemDTO);
                return RedirectToAction(nameof(Index));
            }

            // Reload the item types if validation fails
            var itemTypes = await _itemService.GetAllItemTypesAsync();
            ViewBag.ItemTypes = new SelectList(itemTypes, "Id", "Name");

            return View(itemDTO); // Re-render the form with validation errors
        }



        [Authorize(Policy = "RequireManager")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _itemService.GetItemByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }
        [Authorize(Policy = "RequireManager")]
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


        [Authorize(Policy = "RequireManager")]
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



        [Authorize(Policy = "RequireManager")]
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
                return View(itemDTO); // Render the view with validation errors
            }

            try
            {
                itemDTO.Price = Math.Round(itemDTO.Price, 2);
                await _itemService.UpdateItemAsync(id, itemDTO);

                // Redirect to details page after successful update
                return RedirectToAction("Details", new { id = id });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message); // Return 404 if item is not found
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine(ex.Message);
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }











    }
}
