using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NDV_PetLoversClinic.Models.Records
{
    public class Clients
    {
        [Key]
        public int client_Id { get; set; }

        [ForeignKey("Person")]
        public int person_Id { get; set; }

        public DateTime createdAt { get; set; }

        public DateTime? updatedAt { get; set; }

        //Navigation Property

        public Person Person { get; set; }

        public ICollection<Pet> IPet { get; set; } //for one to many
    }
}
