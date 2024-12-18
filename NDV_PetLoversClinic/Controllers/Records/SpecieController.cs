using Microsoft.AspNetCore.Mvc;
using NDV_PetLoversClinic.Models.Records;
using NDV_PetLoversClinic.Repositories.IRepos;
using NDV_PetLoversClinic.ViewModel.Records;

namespace NDV_PetLoversClinic.Controllers.Records
{
    public class SpecieController : Controller
    {
        private readonly ISpecieRepository _specieRepository;

        public SpecieController(ISpecieRepository specieRepository)
        {
            _specieRepository = specieRepository;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _specieRepository.GetAllSpeciesAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Specie specie)
        {

            //Check duplicate Breed Names
            var HashNames = new HashSet<string>();

            foreach(var name in specie.Breeds)
            {
               if(!HashNames.Add(name.breed_Name))
                {
                    ModelState.AddModelError("", "Duplicate breed name is invalid");
                    return View();
                }
                    
            }

            //save to DB 
            var result = await _specieRepository.AddSpecieAsync(specie);

            if (result)
            {
                ModelState.AddModelError("", $"Specie name \"{specie.specie_Name}\" Already exist");
                return View(specie);
            }


            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
           var specie =  await _specieRepository.GetSpecieAsync(id);

            if (specie != null)
            {
                return View(specie);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Specie species)
        {
            if (id != species.specie_Id)
            {
                return BadRequest("Invalid Specie ID.");
            }

            var result = await _specieRepository.UpdateSpecieAsync(species);

            if (result.NotFound)
            {
                return NotFound($"Specie with ID {id} not found.");
            }

            if (result.DuplicateName)
            {
                ModelState.AddModelError("", $"Specie Name \"{species.specie_Name}\" already exists.");
                return View(species);
            }

            TempData["SuccessMessage"] = "Specie updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {

            var specie = await _specieRepository.GetSpecieWithBreedAsync(id);

            if(specie != null)
            {
                return View(new SpecieVM
                {
                    Specie = specie,
                });
            }

            return NotFound(); 
        }
    }
}

