using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            var species = await _specieRepository.GetAllSpeciesAsync();
            return View(new SpecieVM
            {
                SpecieList = species?.Select(s => new SelectListItem
                {
                    Value = s.specie_Id.ToString(),
                    Text = s.specie_Name
                })
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Create(int specie_Id,List<Breed> Breeds, bool fromSpecieDetails)
        {
            //adding of specieID in BreedList
            foreach (var breed in Breeds)
            {
                breed.specie_Id = specie_Id;
            }

            //process breed exist
            var isBreedNameExist = await _breedRepository.BreedNameExistByList(Breeds);


            //if true
            if (isBreedNameExist.Result)
            {
                //if came from specie Details
                if (fromSpecieDetails)
                {
                    //tempdate is for modal references
                    TempData["OpenModal"] = "BreedModal";
                    TempData["ErrorMessage"] = isBreedNameExist.Message;

                    return RedirectToAction("Details","Specie", new { id= specie_Id });
                }
                else
                {
                    ModelState.AddModelError("", isBreedNameExist.Message);

                    var species = await _specieRepository.GetAllSpeciesAsync();
                    return View(new SpecieVM
                    {
                        SpecieList = species?.Select(s => new SelectListItem
                        {
                            Value = s.specie_Id.ToString(),
                            Text = s.specie_Name
                        })
                    });
                }
                
            }
            //add
            await _breedRepository.AddBreedAsync(Breeds);


            //fromSpecieDetails is true and no conflicts
            if (fromSpecieDetails)
            {
                TempData["Notification"] = "Successfully Added";
                return RedirectToAction("Details","Specie", new { id = specie_Id});
            }



            //add tempdata her for success
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var breed = await _breedRepository.GetBreedAsync(id);

            if (breed != null)
            {
                return View(breed);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id , Breed breed)
        {
            if (id != breed.breed_Id)
            {
                return BadRequest("Invalid Breed ID.");
            }

            var exist = await _breedRepository.BreedNameExist(breed);
            if (exist.Result)
            {
                ModelState.AddModelError("", exist.Message);
                return View(breed);
            }


            var result = await _breedRepository.UpdateAsync(breed);

            if (result == null)
            {
                return NotFound();
            }

       
            TempData["Notification"] = "Successfully Updated!";
            return RedirectToAction("Details","Specie", new { id = breed.specie_Id });
        }
    }
}
