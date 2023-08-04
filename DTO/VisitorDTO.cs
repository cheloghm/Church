namespace Church.DTO
{
    public class VisitorDTO
    {
        public string FullName { get; set; }
        public string GuestOf { get; set; }
        public string OtherRemarks { get; set; }
        public DateTime DateEntered { get; set; }
        public List<string> OtherGuests { get; set; }
    }
}
