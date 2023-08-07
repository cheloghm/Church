using Church.DTO;
using Church.Models;

namespace Church.Mapper
{
    public class VisitorMapper
    {
        public VisitorDTO MapToVisitorDTO(Visitor visitor)
        {
            return new VisitorDTO
            {
                FullName = visitor.FullName,
                GuestOf = visitor.GuestOf,
                OtherRemarks = visitor.OtherRemarks,
                DateEntered = visitor.DateEntered,
                OtherGuests = visitor.OtherGuests
            };
        }
    }
}
