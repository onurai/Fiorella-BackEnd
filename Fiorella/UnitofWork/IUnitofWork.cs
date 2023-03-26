using Fiorella.Repository.Interface;

namespace Fiorella.UnitofWork
{
    public interface IUnitofWork
    {
        public IPictureRepository pictureRepository { get; set; }

        public Task Commit();
    }
}
