using Church.Models;
using Church.RepositoryInterfaces;
using Church.ServiceInterfaces;

namespace Church.Services
{
    public class ProfilePhotoService : IProfilePhotoService
    {
        private readonly IProfilePhotoRepository _profilePhotoRepository;

        public ProfilePhotoService(IProfilePhotoRepository profilePhotoRepository)
        {
            _profilePhotoRepository = profilePhotoRepository;
        }

        public async Task<ProfilePhoto> AddProfilePhoto(byte[] photo, string userId)
        {
            var profilePhoto = new ProfilePhoto { Photo = photo, UserId = userId };
            return await _profilePhotoRepository.AddProfilePhoto(profilePhoto);
        }

        public async Task<ProfilePhoto> GetProfilePhotoByUserId(string userId)
        {
            return await _profilePhotoRepository.GetProfilePhotoByUserId(userId);
        }

        public async Task<bool> DeleteProfilePhotoByUserId(string userId)
        {
            return await _profilePhotoRepository.DeleteProfilePhotoByUserId(userId);
        }
    }

}
