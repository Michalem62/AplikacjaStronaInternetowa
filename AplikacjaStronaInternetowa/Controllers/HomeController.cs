using AplikacjaStronaInternetowa.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AplikacjaStronaInternetowa.Controllers
{
    public class HomeController : Controller
    {
        private static List<CartItem> _cart = new List<CartItem>();
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {

            ViewData["CartItemCount"] = _cart.Sum(item => item.Quantity);
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
            var products = _context.Products.Include(p => p.Category).ToList();
            return View(products);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
            var products = _context.Products.Include(p => p.Category).ToList();
            return View("Index", products);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var product = _context.Products.Include(p => p.Category).FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        [HttpPost]
        [HttpGet]
        public IActionResult AddToCart(int productId)
        {
            var product = _context.Products.Find(productId);
            if (product == null)
            {
                return NotFound();
            }

            var cartItem = _cart.FirstOrDefault(p => p.ProductId == productId);
            if (cartItem == null)
            {
                _cart.Add(new CartItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = 1
                });
            }
            else
            {
                cartItem.Quantity += 1;
            }

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Cart()
        {
            return View(_cart);
        }

        // Aktualizacja iloœci produktu w koszyku
        [HttpPost]
        public IActionResult UpdateCart(int productId, int quantity)
        {
            var cartItem = _cart.FirstOrDefault(p => p.ProductId == productId);
            if (cartItem != null)
            {
                if (quantity <= 0)
                {
                    _cart.Remove(cartItem);
                }
                else
                {
                    cartItem.Quantity = quantity;
                }
            }

            return RedirectToAction("Cart");
        }

        [HttpPost]
        public IActionResult ClearCart()
        {
            _cart.Clear();
            return RedirectToAction("Cart");
        }

        public int GetCartItemCount()
        {
            return _cart.Sum(item => item.Quantity);
        }


    }

}
