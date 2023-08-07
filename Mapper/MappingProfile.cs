using AutoMapper;
using Church.DTO;
using Church.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Church.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Request, RequestDTO>();
            CreateMap<RequestDTO, Request>();

            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();

            CreateMap<Visitor, VisitorDTO>();
            CreateMap<VisitorDTO, Visitor>();
        }
    }
}
