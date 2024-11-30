using Microsoft.EntityFrameworkCore;
using NDV_PetLoversClinic.Data;
using NDV_PetLoversClinic.Models;
using NDV_PetLoversClinic.Models.Records;

namespace NDV_PetLoversClinic.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly NDV_PetLoversClinicContext _context;

        public PersonRepository(NDV_PetLoversClinicContext context)
        {
            _context = context;
        }

        public async Task<Person> AddClientAsync(Person person, Clients client)
        {
            //check existing Person
            var existingPerson = await _context.Person.FirstOrDefaultAsync(
                p => p.fname == person.fname &&
                string.IsNullOrEmpty(p.mname) || p.mname == person.mname &&
                p.lname == person.lname
                );

            if (existingPerson != null)
            {
                return null; //already exist
            }
            else
            {
                // If person does not exist, create the new person
                await _context.Person.AddAsync(person);
                await _context.SaveChangesAsync();

                // Now associate the client with the new person
                client.client_Id = person.person_Id;
            }

            // Set metadata fields for the client
            client.createdAt = DateTime.UtcNow;

            // Add the client record
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();

            return person;

        }

        public async Task<IEnumerable<Person>> GetAllClientAsync()
        {
            var allClient = await _context.Person
                .Join(
                    _context.Clients,
                    person => person.person_Id,
                    client => client.person_Id,
                    (person, client) => person
                )
                .ToListAsync();

            return allClient;
        }

        public async Task<Person> GetClientAsync(int id)
        {
            var client = await _context.Person.FindAsync(id);

            if(client != null)
            {
                return client;
            }

            return null;
           
        }

        public Task<Person> UpdateClientAsync(Person person, Clients client)
        {
            throw new NotImplementedException();
        }
    }
}
