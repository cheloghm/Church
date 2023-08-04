using Church.DTO;
using Church.RepositoryInterfaces;
using Church.ServiceInterfaces;

namespace Church.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IProfilePhotoService _profilePhotoService;
        private readonly IRoleService _roleService;

        public UserService(IUserRepository userRepository, IProfilePhotoService profilePhotoService, IRoleService roleService)
        {
            _userRepository = userRepository;
            _profilePhotoService = profilePhotoService;
            _roleService = roleService;
        }

        public async Task<UserDTO> GetUserDetails(string userId)
        {
            var user = await _userRepository.GetUserById(userId);
            var profilePhoto = await _profilePhotoService.GetProfilePhotoByUserId(userId);
            var role = await _roleService.GetRoleById(user.RoleId);

            return new UserDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                RoleId = role.Name,
                ProfilePhoto = profilePhoto.Photo
            };
        }

        public async Task<UserDTO> UpdateUserProfile(string userId, UserDTO userDto)
        {
            var user = await _userRepository.GetUserById(userId);

            if (user == null)
                return null;

            user.FirstName = userDto.FirstName;
            user.MiddleName = userDto.MiddleName;
            user.LastName = userDto.LastName;
            userDto.DOB = userDto.DOB;
            user.Email = userDto.Email;

            await _userRepository.UpdateUser(user);

            return userDto;
        }

        public async Task<bool> ChangeUserRole(string userId, string newRoleId, string currentUserId)
        {
            var currentUser = await _userRepository.GetUserById(currentUserId);
            var user = await _userRepository.GetUserById(userId);

            if (currentUser == null || user == null)
                return false;

            var currentRole = await _roleService.GetRoleById(currentUser.RoleId);

            if (currentRole.Name != "Pastor" || currentRole.Name != "Deacon" || currentRole.Name != "Admin")
                return false;

            user.RoleId = newRoleId;

            await _userRepository.UpdateUser(user);

            return true;
        }
    }
}
