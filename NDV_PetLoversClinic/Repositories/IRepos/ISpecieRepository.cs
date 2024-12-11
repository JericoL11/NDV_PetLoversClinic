using NDV_PetLoversClinic.Models.Records;

namespace NDV_PetLoversClinic.Repositories.IRepos
{
    public interface ISpecieRepository
    {
        Task<IEnumerable<Specie>?> GetAllSpecies(); // NULLABLE
        Task<(bool Result, Specie)> AddSpecieAsync(Specie species);

    }
}
