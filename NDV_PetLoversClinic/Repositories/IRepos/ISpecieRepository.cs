using Microsoft.AspNetCore.Mvc.Rendering;
using NDV_PetLoversClinic.Classes;
using NDV_PetLoversClinic.Models.Records;

namespace NDV_PetLoversClinic.Repositories.IRepos
{
    public interface ISpecieRepository
    { 
        Task<IEnumerable<Specie>?> GetAllSpeciesAsync(); // NULLABLE
        Task<Specie> AddSpecieAsync(Specie species);

        Task<Specie>? GetSpecieAsync(int id);

        //for details
        Task<Specie>? GetSpecieWithBreedAsync(int id);

        Task<Specie> UpdateSpecieAsync(Specie species);

        Task<ValidationResponse> SpecieExist(Specie species);

    }
}
