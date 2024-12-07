using NDV_PetLoversClinic.Classes;
using NDV_PetLoversClinic.Models.Records;
using NDV_PetLoversClinic.Models;

namespace NDV_PetLoversClinic.Repositories.IRepos
{
    public interface IClientRepository
    {
        // Interface
        Task<IEnumerable<ClientDto>> GetAllClientAsync(string searchTerm);
        Task<(bool Result, Person)> UpdateClientAsync(Person person);
        Task<Person> GetClientAsync(int id);

        Task<(bool Result, Person ExistingPerson)> CheckClient(Person person);
        Task<Person> AddClientAsync(Person person, IList<Pet> pets);
        Task <int> GetAge(DateTime? bdate);
    }
}
