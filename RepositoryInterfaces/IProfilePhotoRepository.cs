using Church.Models;
using System.Threading.Tasks;

namespace Church.RepositoryInterfaces
{
    public interface IProfilePhotoRepository
    {
        Task<ProfilePhoto> AddProfilePhoto(ProfilePhoto profilePhoto);
        Task<ProfilePhoto> GetProfilePhotoByUserId(string userId);
        Task<bool> DeleteProfilePhotoByUserId(string userId);
    }
}
