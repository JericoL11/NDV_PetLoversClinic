using Microsoft.AspNetCore.Mvc.Rendering;
using NDV_PetLoversClinic.Models.Records;

namespace NDV_PetLoversClinic.ViewModel.Records
{
    public class SpecieVM
    {
        public Specie Specie { get; set; }

        public IEnumerable <Breed> IBreed{ get; set; }

        public Breed Breed { get; set; }

        public int? SelectedSpecieId { get; set; }
        public List<SelectListItem> SpecieList { get; set; }
    }
}
