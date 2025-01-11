using AplikacjaStronaInternetowa.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AplikacjaStronaInternetowa.Controllers
{
    
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        private bool IsAdmin()
        {
            return HttpContext.Session.GetString("UserRole") == "Admin";
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            if (!IsAdmin())
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            if (!IsAdmin())
            {
                return RedirectToAction("AccessDenied", "Account");
            }
            if(HttpContext.Session.GetString("UserRole") == "Admin")
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
            return View(product);
                   
        }

        [HttpGet]
        public IActionResult EditProduct(int id)
        {
            if (!IsAdmin())
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        [HttpPost]
        public IActionResult EditProduct(Product product)
        {
            if (!IsAdmin())
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            if (HttpContext.Session.GetString("UserRole") == "Admin")
            {
                _context.Products.Update(product);
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        [HttpPost]
        public IActionResult DeleteProduct(int id)
        {
            if (!IsAdmin())
            {
                return RedirectToAction("AccessDenied", "Account");
            }

            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }


}
