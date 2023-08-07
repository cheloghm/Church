using Church.DTO;
using Church.Models;

namespace Church.Mapper
{
    public class RequestMapper
    {
        public RequestDTO MapToRequestDTO(Request request)
        {
            return new RequestDTO
            {
                NameOfRequestor = request.NameOfRequestor,
                Title = request.Title,
                OtherRemarks = request.OtherRemarks,
                DateEntered = request.DateEntered
            };
        }
    }
}
