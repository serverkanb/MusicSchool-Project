using Microsoft.AspNetCore.Mvc;

namespace MusicSchool.Controllers
{
    public class MusicSchoolContact : Controller
    {
        public IActionResult Contact()
        {
            return View();
        }
    }
}
