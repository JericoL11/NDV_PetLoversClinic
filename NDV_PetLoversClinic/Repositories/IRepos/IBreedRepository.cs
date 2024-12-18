using NDV_PetLoversClinic.Classes;
using NDV_PetLoversClinic.Models.Records;

namespace NDV_PetLoversClinic.Repositories.IRepos
{
    public interface IBreedRepository
    {
        Task<Breed>AddBreedAsync(List<Breed> Breeds);

        //temporary method 
        Task<IEnumerable<Breed>?> SelectListBreedsAsync(int specieId);

        Task<IEnumerable<Breed>?> GetAllAsync();

        Task<ValidationResponse> BreedNameExist(List<Breed>? breedList);


    }
}
