using Microsoft.AspNetCore.Mvc;

namespace PieInfo.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
