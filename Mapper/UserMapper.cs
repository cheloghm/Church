using Church.DTO;
using Church.Models;
using Church.ServiceInterfaces;
using Church.Services;

namespace Church.Mapper
{
    public class UserMapper
    {
        private readonly IProfilePhotoService _profilePhotoService;

        public UserMapper(IProfilePhotoService profilePhotoService)
        {
            _profilePhotoService = profilePhotoService;
        }

        public UserDTO MapToUserDTO(User user)
        {
            // Assuming ProfilePhoto is a byte array, and you have a method to retrieve it by user ID
            var profilePhoto = _profilePhotoService.GetProfilePhotoByUserId(user.Id).Result;

            return new UserDTO
            {
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                Email = user.Email,
                RoleId = user.RoleId,
                DOB = user.DOB,
                ProfilePhoto = profilePhoto.Photo // Assuming the profile photo is a byte array
            };
        }
    }
}
