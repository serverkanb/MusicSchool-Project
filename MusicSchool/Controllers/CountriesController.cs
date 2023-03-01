using Microsoft.AspNetCore.Mvc;

namespace MusicSchool.Controllers
{
    public class CountriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
