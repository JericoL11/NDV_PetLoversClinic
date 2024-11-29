using Microsoft.AspNetCore.Mvc;

namespace NDV_PetLoversClinic.Controllers.Records
{
    public class Client : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
