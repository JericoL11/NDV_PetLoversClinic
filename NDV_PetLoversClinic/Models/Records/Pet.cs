using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NDV_PetLoversClinic.Classes;
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
        public Gender gender { get; set; }

        [ForeignKey("Breed")]
        [Display(Name = "Breed")]
        public int? breed_Id { get; set; }


        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        public DateTime bdate { get; set; }


        [Display(Name ="Color")]
        public string color { get; set; }


        //Navigation Property
        public Clients Client { get; set; }

        public Breed Breed { get; set; }

    }
}
