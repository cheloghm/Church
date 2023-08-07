using Church.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Church.ServiceInterfaces
{
    public interface IUserService
    {
        Task<UserDTO> GetUserDetails(string userId);
        Task<IEnumerable<UserDTO>> GetAllUsers();
        Task<IEnumerable<UserDTO>> GetUsersByRole(string roleId);
        Task<IEnumerable<UserDTO>> SearchUsersByName(string name);
        Task<UserDTO> UpdateUserProfile(string userId, UserDTO userDto);
        Task<bool> ChangeUserRole(string userId, string newRoleId, string currentUserId);
    }
}
