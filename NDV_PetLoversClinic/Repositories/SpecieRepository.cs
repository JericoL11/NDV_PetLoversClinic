using Microsoft.EntityFrameworkCore;
using NDV_PetLoversClinic.Data;
using NDV_PetLoversClinic.Models.Records;
using NDV_PetLoversClinic.Repositories.IRepos;

namespace NDV_PetLoversClinic.Repositories
{
    public class SpecieRepository : ISpecieRepository
    {
        private readonly NDV_PetLoversClinicContext _context;

        public SpecieRepository(NDV_PetLoversClinicContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Specie>?> GetAllSpecies()
        {
            var allSpecie = await _context.Species.ToListAsync();

            return allSpecie;   
        }
        public async Task<(bool Result, Specie)> AddSpecieAsync(Specie species)
        {
            var checkName = await _context.Species.AnyAsync(s => s.specie_Name == species.specie_Name);

            if (checkName)
            {
                return (true, species);
            }

            _context.Species.Add(species);
            await _context.SaveChangesAsync();

            return (false, null);
        }

    }
}
