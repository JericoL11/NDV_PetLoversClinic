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

        public async Task<ValidationResponse> BreedNameExistByList(List<Breed>? breedList)
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
                        Message = $"The name \"{breed.breed_Name}\" is already exist",
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

        public async Task<IEnumerable<Breed>?> GetBreedsBySpecieAsync(int specieId)
        {
            return await _context.Breeds.Where(b => b.specie_Id == specieId).ToListAsync();

        }

        public async Task<Breed> GetBreedAsync(int id)
        {
            var breed = await _context.Breeds.FindAsync(id);

            if (breed != null)
            {
                return breed;
            }

            return null;
        }

        public async Task<ValidationResponse> BreedNameExist(Breed breed)
        {
            //IF ID is not equal to default id and name is equal to existing name
            var exist = await _context.Breeds.AnyAsync(b => b.breed_Id != breed.breed_Id && b.breed_Name == breed.breed_Name);

            if (exist)
            {
                return new ValidationResponse { Result = true, Message = $"{breed.breed_Name} is already exist." };
            }

            return new ValidationResponse { Result = false };
        }

        public async Task<Breed> UpdateAsync(Breed breed)
        {
            var findAsync = await _context.Breeds.FindAsync(breed.breed_Id);

            if (findAsync == null)
            {
                return null;
            };

            findAsync.breed_Name = breed.breed_Name;

            try
            {
                await _context.SaveChangesAsync();
                return breed;
            }
            // tell you that there's a conflict because two people tried to change the same data at once. 
            catch (DbUpdateConcurrencyException)
            {
                return null;
            }
          

        }
    }
}
