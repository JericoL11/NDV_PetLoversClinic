using Microsoft.AspNetCore.Mvc;

namespace NDV_PetLoversClinic.Controllers.Records
{
    public class BreedController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
