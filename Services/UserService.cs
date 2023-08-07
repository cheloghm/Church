using AutoMapper;
using Church.DTO;
using Church.RepositoryInterfaces;
using Church.ServiceInterfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Church.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IProfilePhotoService _profilePhotoService;
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper; // Use AutoMapper's IMapper interface

        public UserService(IUserRepository userRepository, IProfilePhotoService profilePhotoService, IRoleService roleService, IMapper mapper)
        {
            _userRepository = userRepository;
            _profilePhotoService = profilePhotoService;
            _roleService = roleService;
            _mapper = mapper; // Inject AutoMapper
        }

        public async Task<UserDTO> GetUserDetails(string userId)
        {
            var user = await _userRepository.GetUserById(userId);
            return _mapper.Map<UserDTO>(user); // Use AutoMapper to map
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();
            return _mapper.Map<IEnumerable<UserDTO>>(users); // Use AutoMapper to map
        }

        public async Task<IEnumerable<UserDTO>> GetUsersByRole(string roleName)
        {
            var role = await _roleService.GetRoleByName(roleName);
            var users = await _userRepository.GetUsersByRoleId(role.Id);
            return _mapper.Map<IEnumerable<UserDTO>>(users); // Use AutoMapper to map
        }

        public async Task<IEnumerable<UserDTO>> SearchUsersByName(string name)
        {
            var users = await _userRepository.SearchUsersByName(name);
            return _mapper.Map<IEnumerable<UserDTO>>(users); // Use AutoMapper to map
        }

        public async Task<UserDTO> UpdateUserProfile(string userId, UserDTO userDto)
        {
            var user = await _userRepository.GetUserById(userId);

            if (user == null)
                return null;

            _mapper.Map(userDto, user); // Use AutoMapper to map properties from DTO to user

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
