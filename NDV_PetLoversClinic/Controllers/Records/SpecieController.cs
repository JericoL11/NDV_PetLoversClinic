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

            //check if name already exist
            var exist = await _specieRepository.SpecieExist(specie);

            if (exist.Result)
            {
                ModelState.AddModelError("", exist.Message);
                return View();
            }


            //Check duplicate Breed Name on input based on view
            var breedNames = specie.Breeds.Select(s => s.breed_Name).ToList();

            // Check if the number of distinct breed names is the same as the total number of breed names
            if (breedNames.Count != breedNames.Distinct().Count())
            {
                ModelState.AddModelError("", "Duplicate breed name is invalid");
                return View();
            }


            //save to DB 
            var result = await _specieRepository.AddSpecieAsync(specie);

      
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

            //exist
            var exist = await _specieRepository.SpecieExist(species);

            if (exist.Result)
            {
                ModelState.AddModelError("", exist.Message);
                return View();
            }

            //update
            var result = await _specieRepository.UpdateSpecieAsync(species);

            if (result == null)
            {
                return NotFound();
            }
           
            TempData["Notification"] = "Successfully Updated";
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

