using System.ComponentModel.DataAnnotations;

namespace NDV_PetLoversClinic.Models.Records
{
    public class Specie
    {
        [Key]
        public int specie_Id { get; set; }

        [Display(Name = "Specie")]
        public string specie_Name { get; set; }

        //navigation

        public ICollection<Breed> Breeds { get; set; }
    }
}
