using Fiorella.Repository.Interface;

namespace Fiorella.UnitofWork
{
    public interface IUnitofWork
    {
        public IPictureRepository pictureRepository { get; set; }
        public IUserRepository userRepository { get; set; }

        public Task Commit();
    }
}
