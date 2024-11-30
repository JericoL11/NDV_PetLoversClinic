using NDV_PetLoversClinic.Models;
using NDV_PetLoversClinic.Models.Records;
using System.Collections;

namespace NDV_PetLoversClinic.Repositories
{
    public interface IPersonRepository
    {
        //Client
        Task<IEnumerable<Person>> GetAllClientAsync();
        Task<Person> AddClientAsync(Person person,Clients client);
        Task<Person> UpdateClientAsync(Person person, Clients client);
        Task<Person> GetClientAsync(int id);
    }
}
