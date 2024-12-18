using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NDV_PetLoversClinic.Data;
using NDV_PetLoversClinic.Models;
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
        public async Task<bool> AddSpecieAsync(Specie species)
        {
            var checkName = await _context.Species.AnyAsync(s => s.specie_Name == species.specie_Name);

            if (checkName)
            {
                return true;
            }

            _context.Species.Add(species);
            await _context.SaveChangesAsync();

            return false;
        }

        public async Task<(bool NotFound, bool DuplicateName)> UpdateSpecieAsync(Specie species)
        {
            // Find the specie by ID
            var findSpecie = await _context.Species.FindAsync(species.specie_Id);

            if (findSpecie == null)
            {
                return (true, false); // Not Found
            }

            // Check for duplicate name
            var duplicateNameExists = await _context.Species
                .AnyAsync(s => s.specie_Name == species.specie_Name && s.specie_Id != species.specie_Id);

            if (duplicateNameExists)
            {
                return (false, true); // Duplicate Name Exists
            }

            // Update properties
            findSpecie.specie_Name = species.specie_Name;

            try
            {
                await _context.SaveChangesAsync();
                return (false, false); // Update Successful
            }
            catch (DbUpdateConcurrencyException)
            {
                return (true, false); // Record might have been updated/deleted concurrently
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

        public async Task<List<SelectListItem>?> GetSpecieSelectList()
        {
            return await _context.Species
                 .Select(s => new SelectListItem
                 {
                     Value = s.specie_Id.ToString(),
                     Text = s.specie_Name,
                 }).ToListAsync();
        }
    }
}
