using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NDV_PetLoversClinic.Models.Records
{
    public class Pet
    {
        [Key]
        public int pet_Id { get; set; }

        [ForeignKey("Client")]
        public int client_Id { get; set; }

        [Display(Name = "Name")]
        public string name { get; set; }

        [Display(Name = "Gender")]
        public string gender { get; set; }

        [Display(Name = "Breed")]
        public string breed { get; set; }

        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        public DateTime bdate { get; set; }

        [Display(Name = "Age")]
        public int age { get; set; }

        [Display(Name = "Specie")]
        public string specie { get; set; }


        //Navigation Property
        public Clients Client { get; set; }

    }
}
