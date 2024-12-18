using Microsoft.AspNetCore.Mvc.Rendering;
using NDV_PetLoversClinic.Models.Records;

namespace NDV_PetLoversClinic.Repositories.IRepos
{
    public interface ISpecieRepository
    {
        Task<IEnumerable<Specie>?> GetAllSpeciesAsync(); // NULLABLE
        Task<bool> AddSpecieAsync(Specie species);

        Task<Specie>? GetSpecieAsync(int id);

        Task<Specie>? GetSpecieWithBreedAsync(int id);

        Task<(bool NotFound, bool DuplicateName)> UpdateSpecieAsync(Specie species);

        Task<List<SelectListItem>?> GetSpecieSelectList();

   


    }
}
