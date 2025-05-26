using Microsoft.EntityFrameworkCore;
using SharedLib.Data;

namespace SharedLib.Service
{
    public class PhotoService : IPhotoService
    {
        private readonly IDbContextFactory<GotorzContext> _dbContextFactory;

        public PhotoService(IDbContextFactory<GotorzContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<bool> Add(Photo photo)
        {
            try
            {
                using var context = _dbContextFactory.CreateDbContext();
                context.Photos.Add(photo);
                await context.SaveChangesAsync();
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
            using var context = _dbContextFactory.CreateDbContext();
            var photo = await context.Photos.FindAsync(id);
            if (photo == null) return false;

            context.Photos.Remove(photo);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Photo>> GetAll()
        {
            using var context = _dbContextFactory.CreateDbContext();
            return await context.Photos.ToListAsync();
        }

        public async Task<Photo> GetById(int id)
        {
            using var context = _dbContextFactory.CreateDbContext();
            return await context.Photos.FindAsync(id);
        }

        public async Task<bool> Update(Photo photo)
        {
            using var context = _dbContextFactory.CreateDbContext();
            var existing = await context.Photos.FindAsync(photo.Id);
            if (existing == null) return false;

            existing.PhotoName = photo.PhotoName;
            existing.PhotoData = photo.PhotoData;

            context.Photos.Update(existing);
            await context.SaveChangesAsync();
            return true;
        }
    }
}