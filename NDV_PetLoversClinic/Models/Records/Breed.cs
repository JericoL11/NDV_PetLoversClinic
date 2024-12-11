using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NDV_PetLoversClinic.Models.Records
{
    public class Breed
    {
        [Key]
        public int breed_Id { get; set; }

        [Display(Name ="Breed Name")]
        public string breed_Name { get; set; }

        [ForeignKey("Specie")]
        public int specie_Id { get; set; }


        //navigation Property
        public Specie Specie { get; set; }
    }
}
