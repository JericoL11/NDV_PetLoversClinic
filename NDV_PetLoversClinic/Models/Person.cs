using NDV_PetLoversClinic.Classes;
using NDV_PetLoversClinic.Models.Records;
using System.ComponentModel.DataAnnotations;

namespace NDV_PetLoversClinic.Models
{
    public class Person
    {
        [Key]
        [Display(Name = "Person ID")]
        public int person_Id { get; set; }

        [Display(Name = "First Name")]
        public string? fname {  get; set; }

        [Display(Name = "Middle Name")]
        public string? mname { get; set; }

        [Display(Name = "Last Name")]
        public string? lname { get; set; }

        [Display(Name = "Gender")]
        public Gender gender { get; set; }

        [Display(Name = "Address")]
        public string ?address { get; set; }

        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        public DateTime? bdate { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string? email { get; set; }

        //navigation for contact
        public ICollection<Contact> IContact { get; set; }


    }
}
