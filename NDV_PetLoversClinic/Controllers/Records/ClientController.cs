using Microsoft.AspNetCore.Mvc;
using NDV_PetLoversClinic.Repositories;

namespace NDV_PetLoversClinic.Controllers.Records
{
    public class ClientController : Controller
    {
        private readonly IPersonRepository _personRepository;

        public ClientController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public IActionResult Index()
        {
          var getAll = _personRepository.GetAllClientAsync();

            return View();
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
