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
        private readonly ISpecieRepository _specieRepository;
        private readonly IBreedRepository _breedRepository;

        public ClientController(IClientRepository clientRepository, ISpecieRepository specieRepository, IBreedRepository breedRepository)
        {
            _clientRepository = clientRepository;
            _specieRepository = specieRepository;
            _breedRepository = breedRepository;
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

     

        //FUNCTION 
        public async Task CustomViewBags()
        {
            //gender
            ViewBag.GenderOptions = new SelectList(Enum.GetValues(typeof(Gender)));

            //Specie
            ViewBag.SpecieOptions = (await _specieRepository.GetAllSpeciesAsync())?
             .Select(s => new SelectListItem
             {
                 Value = s.specie_Id.ToString(), // The value sent to the server
                 Text = s.specie_Name                // The text shown to the user
             });

        }

        public async Task<IActionResult> GetBreedsBySpecie(int specieId)
        {
            var breeds = await _breedRepository.SelectListBreedsAsync(specieId);

            var breedList = breeds.Select(b => new
            {
                value = b.breed_Id.ToString(),
                text = b.breed_Name
            }).ToList();

            return Json(breedList);
        }



        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await CustomViewBags();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(List<Pet> Pet, Person persons)
        {
            await CustomViewBags();

            // Call the repository method
            var IsClientExist = await _clientRepository.IsClientExist(persons);

            if (IsClientExist)
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

                var vm = new ClientVm
                {
                    Person = getClient,
                    FullName = $"{getClient.fname} {getClient.mname} {getClient.lname}",
                    Age = getAge,
                    Contact = string.Join(", ", getClient.IContact.Select(s => s.contactNo))
                };

                return View(vm);
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

