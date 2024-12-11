using Microsoft.AspNetCore.Mvc;
using NDV_PetLoversClinic.Models.Records;
using NDV_PetLoversClinic.Repositories.IRepos;

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
            return View(await _specieRepository.GetAllSpecies());
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
                    ModelState.AddModelError("", "Duplicate breed name ame is invalid");
                    return View();
                }
                    
            }

            //save to DB 
            var result = await _specieRepository.AddSpecieAsync(specie);

            if (result.Result)
            {
                ModelState.AddModelError("", $"Specie name \"{specie.specie_Name}\" Already exist");
                return View(specie);
            }


            return RedirectToAction(nameof(Index));
        }
    }
}

