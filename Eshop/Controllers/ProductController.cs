using Microsoft.AspNetCore.Mvc;

namespace Eshop.Controllers;

public class ProductController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}