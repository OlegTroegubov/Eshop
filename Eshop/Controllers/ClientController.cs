using Microsoft.AspNetCore.Mvc;

namespace Eshop.Controllers;

public class ClientController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}