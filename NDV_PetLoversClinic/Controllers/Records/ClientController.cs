using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NDV_PetLoversClinic.Classes;
using NDV_PetLoversClinic.Models;
using NDV_PetLoversClinic.Models.Records;
using NDV_PetLoversClinic.Repositories.IRepos;
using NDV_PetLoversClinic.ViewModel.Records;

namespace NDV_PetLoversClinic.Controllers.Records
{
    public class ClientController : Controller
    {
        private readonly IClientRepository _clientRepository;

        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchTerm)
        {
            // Initialize a default ViewModel
            var model = new ClientVm { isFiltered = false };

            if (!string.IsNullOrEmpty(searchTerm))
            {
                try
                {
                    // Get DTOs from the service
                    var getAllClient = await _clientRepository.GetAllClientAsync(searchTerm);
                    // Prepare ViewModel for the view
                    model.IEClients = getAllClient;
                    model.isFiltered = true;
                    model.searchTerm = searchTerm;
                }
                catch (Exception ex)
                {
                    // Handle errors
                    Console.WriteLine($"Error: {ex.Message}");
                    ModelState.AddModelError("", "An error occurred while fetching data.");
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            //gender
            ViewBag.GenderOptions = new SelectList(Enum.GetValues(typeof(Gender)));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(List<Pet> Pet, Person persons)
        {
            //after "," is the selected  gender
            ViewBag.GenderOptions = new SelectList(Enum.GetValues(typeof(Gender)),persons.gender);


            // Call the repository method
            var repo = await _clientRepository.CheckClient(persons);

            if (!repo.Result)
            {
                ModelState.AddModelError(string.Empty, "A person with the same name already exists.");

                return View(persons);
            }

            await _clientRepository.AddClientAsync(persons, Pet);
            return RedirectToAction("Index"); 
        }

        public async Task<IActionResult> ViewClient(int id)
        {
            var getClient = await _clientRepository.GetClientAsync(id);

            if (getClient != null)
            {
                //get age
                var getAge =  await _clientRepository.GetAge(getClient.bdate.Value);

                return View(new ClientVm
                {
                    Person = getClient,
                    FullName = $"{getClient.fname} {getClient.mname} {getClient.lname}",
                    Age = getAge

                });
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (string.IsNullOrWhiteSpace(id.ToString()))
            {
                return NotFound(); 
            }

            var client = await _clientRepository.GetClientAsync(id);

            if (client != null)
            {
                return View(client);
            }

            return View();
         
        }

    }
}

