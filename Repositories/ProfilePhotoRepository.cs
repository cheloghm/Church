using Church.Data;
using Church.Models;
using Church.RepositoryInterfaces;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Church.Repositories
{
    public class ProfilePhotoRepository : IProfilePhotoRepository
    {
        private readonly DataContext _context;

        public ProfilePhotoRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ProfilePhoto> AddProfilePhoto(ProfilePhoto profilePhoto)
        {
            await _context.ProfilePhotos.InsertOneAsync(profilePhoto);
            return profilePhoto;
        }

        public async Task<ProfilePhoto> GetProfilePhotoByUserId(string userId)
        {
            return await _context.ProfilePhotos.Find(p => p.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteProfilePhotoByUserId(string userId)
        {
            var result = await _context.ProfilePhotos.DeleteOneAsync(p => p.UserId == userId);
            return result.DeletedCount > 0;
        }
    }

}
