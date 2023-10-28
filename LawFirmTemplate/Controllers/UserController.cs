using Microsoft.AspNetCore.Mvc;

namespace LawFirmTemplate.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
