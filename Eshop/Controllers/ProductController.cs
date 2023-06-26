using Eshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Eshop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Product
        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var products = await _context.Products.ToListAsync(cancellationToken);
            return View(products);
        }
        
        [HttpGet]
        public async Task<IActionResult> Details(int id, CancellationToken cancellationToken, string errorMessage = null)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
            ViewBag.ErrorMessages = errorMessage;
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync(cancellationToken);
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public async Task<IActionResult> Edit(Product product, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                // Получение списка ошибок валидации
                var errorMessages = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                // Создание строки, объединяющей ошибки валидации
                var errorMessage = string.Join(", ", errorMessages);

                return RedirectToAction("Details", new { id = product.Id, errorMessage });
            }

            _context.Products.Update(product);
            await _context.SaveChangesAsync(cancellationToken);
            return RedirectToAction("Details", new { id = product.Id });
        }
    }
}
