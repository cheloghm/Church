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
            var profilePhoto = await _profilePhotoService.GetProfilePhotoByUserId(userId); // Fetch the profile photo
            var role = await _roleService.GetRoleById(user.RoleId); // Fetch the role using RoleId

            var userDto = _mapper.Map<UserDTO>(user);
            userDto.ProfilePhoto = profilePhoto?.Photo; // Assign the profile photo to the DTO
            userDto.Role = role?.Name; // Assign the role name to the DTO

            return userDto; // Use AutoMapper to map
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();
            var userDtos = users.Select(async user => {
                var role = await _roleService.GetRoleById(user.RoleId);
                var userDto = _mapper.Map<UserDTO>(user);
                userDto.Role = role?.Name;
                return userDto;
            }).ToList();

            return await Task.WhenAll(userDtos); // Wait for all tasks to complete
        }

        public async Task<IEnumerable<UserDTO>> GetUsersByRole(string roleName)
        {
            var role = await _roleService.GetRoleByName(roleName);
            var users = await _userRepository.GetUsersByRoleId(role.Id);
            var userDtos = users.Select(async user => {
                var userDto = _mapper.Map<UserDTO>(user);
                userDto.Role = role?.Name;
                return userDto;
            }).ToList();

            return await Task.WhenAll(userDtos); // Wait for all tasks to complete
        }

        public async Task<IEnumerable<UserDTO>> SearchUsersByName(string name)
        {
            var users = await _userRepository.SearchUsersByName(name);
            var userDtos = users.Select(async user => {
                var role = await _roleService.GetRoleById(user.RoleId);
                var userDto = _mapper.Map<UserDTO>(user);
                userDto.Role = role?.Name;
                return userDto;
            }).ToList();

            return await Task.WhenAll(userDtos); // Wait for all tasks to complete
        }

        public async Task<UserDTO> UpdateUserProfile(string userId, UserDTO userDto)
        {
            var user = await _userRepository.GetUserById(userId);

            if (user == null)
                return null;

            _mapper.Map(userDto, user);

            await _userRepository.UpdateUser(user);

            var role = await _roleService.GetRoleById(user.RoleId); // Fetch the role using RoleId
            userDto.Role = role?.Name; // Assign the role name to the DTO

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
