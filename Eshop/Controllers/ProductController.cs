using Eshop.Models;
using Eshop.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace Eshop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ProductCategoryService _categoryService;
        public ProductController(ApplicationDbContext context, ProductCategoryService categoryService)
        {
            _context = context;
            _categoryService = categoryService;
        }

        // GET: Product
        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var products = await _context.Products.
                Include(p => p.ProductCategory).
                ToListAsync(cancellationToken);

            return View(products);
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(Product product, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.ToDictionary(x => x.Key, x => x.Value.Errors.Select(e => e.ErrorMessage).ToArray());
                return BadRequest(errors);
            }

            await _context.Products.AddAsync(product, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            // Формируем URL для перенаправления
            var redirectUrl = Url.Action("Details", new { id = product.Id });

            // Возвращаем JSON-ответ с URL для перенаправления
            return Json(new { redirectUrl });
        }
        
        [HttpGet]
        public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
        {
            var product = await _context.Products.
                Include(p => p.ProductCategory).
                FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
            
            var categories = await _context.ProductCategories.ToListAsync(cancellationToken);
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
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
                var errors = ModelState.ToDictionary(x => x.Key, x => x.Value.Errors.Select(e => e.ErrorMessage).ToArray());
                return BadRequest(errors);
            }

            _context.Products.Update(product);
            await _context.SaveChangesAsync(cancellationToken);

            // Формируем URL для перенаправления
            var redirectUrl = Url.Action("Details", new { id = product.Id });

            // Возвращаем JSON-ответ с URL для перенаправления
            return Json(new { redirectUrl });
        }
        
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            return Json(await _categoryService.GetCategoriesAsync());
        }
    }
}
