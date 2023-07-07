using Cinema.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CinemaApp.Web.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {

        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return View(_shoppingCartService.GetTicketForShoppingCart(userId));
        }

        public IActionResult Delete(Guid? id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!string.IsNullOrEmpty(userId))
            {
                _shoppingCartService.DeleteTicketFromShoppingCart(id, userId);
            }

            return RedirectToAction("Index", "ShoppingCart");
        }

        public IActionResult OrderNow()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!string.IsNullOrEmpty(userId))
            {
                _shoppingCartService.CreateOrder(userId);
            }

            return RedirectToAction("Index", "Products");
        }
    }
}
