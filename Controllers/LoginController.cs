using Althaus_Warehouse.Models; // Ensure this namespace is correct
using Microsoft.AspNetCore.Mvc;

namespace Althaus_Warehouse.Controllers
{
    public class LoginController : Controller
    {
        // GET: /Login
        public IActionResult Index()
        {
            return View(new LoginViewModel()); // Return an empty view model
        }

        // POST: /Login
        [HttpPost]
        public IActionResult Index(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Implement your authentication logic here
                // If successful:
                // return RedirectToAction("Index", "Home");
                // If not successful:
                // ModelState.AddModelError("", "Invalid login attempt.");
            }

            return View(model); // Return the view with validation errors
        }
    }
}
