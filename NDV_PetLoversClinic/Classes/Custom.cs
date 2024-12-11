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

}
