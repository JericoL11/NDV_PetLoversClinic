using Microsoft.EntityFrameworkCore;
using NDV_PetLoversClinic.Data;
using NDV_PetLoversClinic.Models.Records;
using NDV_PetLoversClinic.Repositories.IRepos;

namespace NDV_PetLoversClinic.Repositories
{
    public class BreedRepository : IBreedRepository
    {
        private readonly NDV_PetLoversClinicContext _context;

        public BreedRepository(NDV_PetLoversClinicContext context)
        {
            _context = context;
        }
        public async Task<(bool Result, Breed)> AddBreedAsycn(Breed breeds)
        {

            var checkName = await _context.Breeds.AnyAsync(b => b.breed_Name == breeds.breed_Name);

            if (checkName)
            {
                return (true, breeds);
            }

            _context.Breeds.Add(breeds);
            await _context.SaveChangesAsync();

            return (false, null);
        }

    }
}
