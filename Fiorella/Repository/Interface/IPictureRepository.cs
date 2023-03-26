using Fiorella.Data.Entity;
using Fiorella.Dto;

namespace Fiorella.Repository.Interface
{
    public interface IPictureRepository : IRepository<Picture, int>
    {
        Task<List<Picture>> GetAll();
        Task<Picture> Get(int id);
        Task<Picture> Create(PictureDto empDto);
        Task<bool> Update(int id, PictureDto empDto);
        Task<bool> Remove(int id);
    }
}
