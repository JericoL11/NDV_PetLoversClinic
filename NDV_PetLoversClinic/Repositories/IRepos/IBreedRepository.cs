using NDV_PetLoversClinic.Classes;
using NDV_PetLoversClinic.Models.Records;

namespace NDV_PetLoversClinic.Repositories.IRepos
{
    public interface IBreedRepository
    {
        Task<Breed>AddBreedAsync(List<Breed> Breeds);

        Task<IEnumerable<Breed>?> GetAllAsync();

        //for creation of breed
        Task<ValidationResponse> BreedNameExistByList(List<Breed>? breedList);

        Task<IEnumerable<Breed>?> GetBreedsBySpecieAsync(int specieId);

        Task<Breed> GetBreedAsync(int specieId);
        Task <Breed> UpdateAsync(Breed breed);

        //for update
        Task<ValidationResponse> BreedNameExist(Breed breed);

    }
}
