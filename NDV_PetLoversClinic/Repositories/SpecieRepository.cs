using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NDV_PetLoversClinic.Data;
using NDV_PetLoversClinic.Classes;
using NDV_PetLoversClinic.Models.Records;
using NDV_PetLoversClinic.Repositories.IRepos;
using System.Data;

namespace NDV_PetLoversClinic.Repositories
{
    public class SpecieRepository : ISpecieRepository
    {
        private readonly NDV_PetLoversClinicContext _context;

        public SpecieRepository(NDV_PetLoversClinicContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Specie>?> GetAllSpeciesAsync()
        {
            var allSpecie = await _context.Species.ToListAsync();

            return allSpecie;   
        }
        public async Task<Specie> AddSpecieAsync(Specie species)
        {

            _context.Species.Add(species);
            await _context.SaveChangesAsync();

            return species; 
        }

        public async Task<Specie> UpdateSpecieAsync(Specie species)
        {
            // Find the specie by ID
            var findSpecie = await _context.Species.FindAsync(species.specie_Id);

            if (findSpecie == null)
            {
                return null;
            }

            // Update properties
            findSpecie.specie_Name = species.specie_Name;

            try
            {
                await _context.SaveChangesAsync();
                return species; // Update Successful
            }
            catch (DbUpdateConcurrencyException)
            {
                return null; // Record might have been updated/deleted concurrently
            }
        }



        public async Task<Specie>? GetSpecieAsync(int id)
        {
            var specie = await _context.Species.FindAsync(id);

            if (specie == null)
            {
                return null;
            }

            return specie;
        }

        public async Task<Specie>? GetSpecieWithBreedAsync(int id)
        {
            var specie = await _context.Species.Include(s => s.Breeds).FirstOrDefaultAsync(s => s.specie_Id == id);

            if (specie != null)
            {
                return specie;
            }

            return null;
        }
        public async Task<ValidationResponse> SpecieExist(Specie species)
        {

            // Check for duplicate name
            var exist = await _context.Species
                .AnyAsync(s => s.specie_Name == species.specie_Name && s.specie_Id != species.specie_Id);

            if (exist)
            {
                return new ValidationResponse
                {
                    Result = true,
                    Message = $"Specie Name \"{species.specie_Name}\" is already exist"
                }; // Duplicate Name Exists

            }

            return new ValidationResponse { Result = false };

        }

    }
}
