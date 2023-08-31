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

            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.Role, opt => opt.Ignore());
            CreateMap<UserDTO, User>();

            CreateMap<UpdateUserDTO, User>();

            CreateMap<Visitor, VisitorDTO>()
            .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => src.DateCreatedString));

            CreateMap<VisitorDTO, Visitor>()
                .ForMember(dest => dest.DateCreated, opt => opt.Ignore()); // Ignore this field when mapping from DTO to model

            CreateMap<Announcement, AnnouncementDTO>()
            .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => src.DateCreatedString));

            CreateMap<AnnouncementDTO, Announcement>()
                .ForMember(dest => dest.DateCreated, opt => opt.Ignore()); // Ignore this field when mapping from DTO to model

        }
    }
}
