using NDV_PetLoversClinic.Models;
using NDV_PetLoversClinic.Models.Records;
using NDV_PetLoversClinic.Classes;

namespace NDV_PetLoversClinic.ViewModel.Records
{
    public class ClientVm
    {
        public IEnumerable<ClientDto> IEClients { get; set; }
        public Person Person { get; set; }
        public Clients Clients { get; set; }

        public string FullName { get; set; }
        public string? searchTerm { get; set; }
        public bool? isFiltered {  get; set; }

        public int Age {  get; set; }
    }

}
