namespace Church.DTO
{
    public class UserDTO
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public DateTime DOB { get; set; }
        public byte[] ProfilePhoto { get; set; }
    }
}
