using Microsoft.Identity.Client;

namespace NDV_PetLoversClinic.Classes
{
    public class Custom
    {
        //NULL

    } 
    // DTO - Data transfer Object
    public class ClientDto
    {
        public int ClientId { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Contacts { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }

    //for repository response
   public class ValidationResponse
    {
        public bool Result { get; set; }
        public string? Message { get; set; }
        public string? ConflictData { get; set; }
    }

}
