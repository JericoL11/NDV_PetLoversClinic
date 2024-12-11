using NDV_PetLoversClinic.Models.Records;

namespace NDV_PetLoversClinic.Repositories.IRepos
{
    public interface IBreedRepository
    {
        Task<(bool Result, Breed)> AddBreedAsycn(Breed breeds);
    }
}
