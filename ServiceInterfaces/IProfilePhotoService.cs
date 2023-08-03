using Church.Models;

namespace Church.ServiceInterfaces
{
    public interface IProfilePhotoService
    {
        Task<ProfilePhoto> AddProfilePhoto(byte[] photo, string userId);
        Task<ProfilePhoto> GetProfilePhotoByUserId(string userId);
        Task<bool> DeleteProfilePhotoByUserId(string userId);
    }
}
