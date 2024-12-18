using Microsoft.EntityFrameworkCore;
using NDV_PetLoversClinic.Classes;
using NDV_PetLoversClinic.Data;
using NDV_PetLoversClinic.Models.Records;
using NDV_PetLoversClinic.Repositories.IRepos;

namespace NDV_PetLoversClinic.Repositories
{
    public class BreedRepository : IBreedRepository
    {

        //dependency injection
        private readonly NDV_PetLoversClinicContext _context;

        public BreedRepository(NDV_PetLoversClinicContext context)
        {
            _context = context;
        }
        public async Task<Breed> AddBreedAsync(List<Breed> Breeds)
        {
            _context.Breeds.AddRange(Breeds);
            await _context.SaveChangesAsync();
            return null;
        }

        public async Task<ValidationResponse> BreedNameExist(List<Breed>? breedList)
        {

            if(breedList == null || !breedList.Any())
            {
                return (new ValidationResponse
                {
                    Result = false,
                });
            }

            //read each data
            foreach (var breed in breedList)
            {
                //check if exist

                bool exist = await _context.Breeds.AnyAsync(b => b.specie_Id == breed.specie_Id && b.breed_Name == breed.breed_Name);

                //if true
                if (exist)
                {
                    return (new ValidationResponse
                    {
                        Result = true,
                        Message = $"Breed name \"{breed.breed_Name}\" is already exist",
                    });
                }
            }

            return (new ValidationResponse
            {
                Result = false,
            });
        }


        public async Task<IEnumerable<Breed>?> GetAllAsync()
        {
            return await _context.Breeds.Include(b => b.Specie).ToListAsync();
        }

        public async Task<IEnumerable<Breed>?> SelectListBreedsAsync(int specieId)
        {
            return await _context.Breeds.Where(b => b.specie_Id == specieId).ToListAsync();
        }
    }
}
