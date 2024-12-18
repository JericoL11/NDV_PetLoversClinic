using Microsoft.AspNetCore.Mvc;
using NDV_PetLoversClinic.Models.Records;
using NDV_PetLoversClinic.Repositories.IRepos;
using NDV_PetLoversClinic.ViewModel.Records;

namespace NDV_PetLoversClinic.Controllers.Records
{
    public class BreedController : Controller
    {
        private readonly IBreedRepository _breedRepository;
        private readonly ISpecieRepository _specieRepository;

        public BreedController(IBreedRepository breedRepository,ISpecieRepository specieRepository)
        {
            _breedRepository = breedRepository;
            _specieRepository = specieRepository;
        }
        public async Task<IActionResult> Index()
        {
           
            return View(await _breedRepository.GetAllAsync());
        }

        public async Task<IActionResult> Create()
        {
            return View( new SpecieVM
            {
                SpecieList = await _specieRepository.GetSpecieSelectList()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Create(int specie_Id,List<Breed> Breeds)
        {
            //adding of specieID in BreedList
            foreach (var breed in Breeds)
            {
                breed.specie_Id = specie_Id;
            }

            //process breed exist
            var isBreedNameExist = await _breedRepository.BreedNameExist(Breeds);


            //if true
            if (isBreedNameExist.Result)
            {
                ModelState.AddModelError("", isBreedNameExist.Message);
                return View(new SpecieVM
                {
                    SpecieList = await _specieRepository.GetSpecieSelectList(),
                });
            }

            //add
            await _breedRepository.AddBreedAsync(Breeds);

            return RedirectToAction(nameof(Index));
        }
    }
}
