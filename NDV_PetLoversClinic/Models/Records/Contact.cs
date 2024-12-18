using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NDV_PetLoversClinic.Models.Records
{
    public class Contact
    {
        [Key]
        public int contact_Id { get; set; }

        [ForeignKey("Person")]
        [Display(Name = "Person Id")]
        public int person_Id { get; set; }

        [Display(Name ="Contact No.")]
        [MaxLength(11)]
        public string? contactNo { get; set; }

        //navigation
        public Person Person { get; set; }

    }
}
