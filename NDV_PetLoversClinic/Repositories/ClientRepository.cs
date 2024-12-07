using Microsoft.EntityFrameworkCore;
using NDV_PetLoversClinic.Classes;
using NDV_PetLoversClinic.Data;
using NDV_PetLoversClinic.Models;
using NDV_PetLoversClinic.Models.Records;
using NDV_PetLoversClinic.Repositories.IRepos;

namespace NDV_PetLoversClinic.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly NDV_PetLoversClinicContext _context;

        public ClientRepository(NDV_PetLoversClinicContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ClientDto>> GetAllClientAsync(string searchTerm)
        {
            var searchTermLower = searchTerm.ToLower();

            // Query the database and project into DTOs
            var clientList = await _context.Clients
                .Include(p => p.Person)
                .ThenInclude(p => p.IContact)
                .Where(p =>
                    p.Person.fname.ToLower().Contains(searchTermLower) ||
                    p.Person.lname.ToLower().Contains(searchTermLower) ||
                    p.Person.mname.ToLower().Contains(searchTermLower) ||
                    (p.Person.fname + " " + p.Person.lname).ToLower().Contains(searchTermLower) ||
                    (p.Person.fname + p.Person.lname).ToLower().Contains(searchTermLower) ||
                    (p.Person.fname + " " + p.Person.mname + " " + p.Person.lname).ToLower().Contains(searchTermLower)
                )
                .Select(item => new ClientDto
                {
                    ClientId = item.client_Id,
                    FullName = $"{item.Person.fname} {item.Person.mname} {item.Person.lname}",
                    Address = item.Person.address,
                    Contacts = string.Join(", ", item.Person.IContact.Select(p => p.contactNo))
                })
                .ToListAsync();

            return clientList;
        }


        public async Task<Person> GetClientAsync(int id)
        {
            var client = await _context.Person.FirstOrDefaultAsync(p => p.person_Id == id);

            if (client != null)
            {
                return client;
            }

            return null;

        }

        public async Task<(bool Result, Person ExistingPerson)> CheckClient(Person person)
        {
            // Check if person already exists
            var existingPerson = await _context.Person.FirstOrDefaultAsync(
                p =>
                    p.fname == person.fname &&
                    (string.IsNullOrEmpty(person.mname) ? string.IsNullOrEmpty(p.mname) : p.mname == person.mname) &&
                    p.lname == person.lname
            );

            if (existingPerson != null)
            {
                // Return the existing person to indicate it was already present
                return (false, existingPerson);
            }

            return (true, null);
        }

        public async Task<Person> AddClientAsync(Person person, IList<Pet> pets)
        {
            var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Add the new person
                await _context.Person.AddAsync(person);
                await _context.SaveChangesAsync();

                // Create the client record and associate it with the new person
                var client = new Clients
                {
                    person_Id = person.person_Id,
                    createdAt = DateTime.UtcNow,
                    IPet = pets
                };

                await _context.Clients.AddAsync(client);

                // Commit all changes in one SaveChangesAsync call
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return person; // Indicate success
            }
            catch
            {
                await transaction.RollbackAsync();
                throw; // Rethrow exception to be handled by the calling layer
            }
        }

        public async Task<(bool Result, Person)> UpdateClientAsync(Person person)
        {
            return (false, null);
        }


        //function

        public async Task<int> GetAge(DateTime? bdate)
        {
            if (!bdate.HasValue)
            {
                throw new ArgumentNullException(nameof(bdate), "Birthdate cannot be null.");
            }

            var birthDate = bdate.Value;
            var today = DateTime.Today;

            int age = today.Year - birthDate.Year;

            // Adjust if the birthdate hasn't occurred yet this year
            if (birthDate.Date > today.AddYears(-age))
            {
                age--;
            }

            return age;
        }
    }
}
