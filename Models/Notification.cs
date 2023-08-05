namespace Church.Models
{
    public class Notification
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Message { get; set; }
        public DateTime DateCreated { get; set; }
        public bool IsRead { get; set; }
        public bool IsActive { get; set; }
    }

}
