using Church.DTO;

namespace Church.ServiceInterfaces
{
    public interface IUserService
    {
        Task<UserDTO> GetUserDetails(string userId);
        Task<UserDTO> UpdateUserProfile(string userId, UserDTO userDto);
        Task<bool> ChangeUserRole(string userId, string newRoleId, string currentUserId);
    }
}
