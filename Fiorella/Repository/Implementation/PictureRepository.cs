using Fiorella.Data.Context;
using Fiorella.Data.Entity;
using Fiorella.Dto;
using Fiorella.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Fiorella.Repository.Implementation
{
    public class PictureRepository : EfRepository<Picture, int>, IPictureRepository
    {
        private readonly AppDbContext _appDbContext;

        public PictureRepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }

        public async Task<Picture> Create(PictureDto empDto)
        {
            Picture picture = new()
            {
                Name = empDto.Name,
                Source = empDto.Source, 
                Description = empDto.Description,
                Category = empDto.Category,
            };
            _appDbContext.Pictures.Add(picture);
            await _appDbContext.SaveChangesAsync();
            return picture;
        }

        public async Task<List<Picture>> GetAll()
        {
            List<Picture> pictures = await _appDbContext.Pictures.ToListAsync();
            return pictures;
        }

        public Task<Picture> Get(int id)
        {
            var result = _appDbContext.Pictures.FirstOrDefaultAsync(i => i.Id == id);
            return result;
        }

        public async Task<bool> Remove(int id)
        {
            var result = await _appDbContext.Pictures.FirstOrDefaultAsync(x => x.Id == id);
            _appDbContext.Pictures.Remove(result);
            await _appDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(int id, PictureDto empDto)
        {
            Picture picture = await _appDbContext.Pictures.FirstOrDefaultAsync(x => x.Id == id);

            picture.Description = empDto.Description;
            picture.Price = empDto.Price;
            picture.Source = empDto.Source;
            picture.Category = empDto.Category;

            await _appDbContext.SaveChangesAsync();
            return true;
        }
    }
}
