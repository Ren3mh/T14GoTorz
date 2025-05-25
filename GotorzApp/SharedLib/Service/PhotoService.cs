using Microsoft.EntityFrameworkCore;
using SharedLib.Data;

namespace SharedLib.Service
{
    public class PhotoService : IPhotoService
    {
        private readonly GotorzContext _context;

        public PhotoService(GotorzContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(Photo photo)
        {
            try
            {
                _context.Photos.Add(photo);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while adding photo: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> Delete(int id)
        {
            var photo = await _context.Photos.FindAsync(id);
            if (photo == null) return false;

            _context.Photos.Remove(photo);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Photo>> GetAll()
        {
            return await _context.Photos.ToListAsync();
        }

        public async Task<Photo> GetById(int id)
        {
            return await _context.Photos.FindAsync(id);
        }

        public async Task<bool> Update(Photo photo)
        {
            var existing = await _context.Photos.FindAsync(photo.Id);
            if (existing == null) return false;

            existing.PhotoName = photo.PhotoName;
            existing.PhotoData = photo.PhotoData;

            _context.Photos.Update(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}