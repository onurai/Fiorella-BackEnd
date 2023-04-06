using Fiorella.Data.Context;
using Fiorella.Repository.Implementation;
using Fiorella.Repository.Interface;

namespace Fiorella.UnitofWork
{
    public class UnitofWork : IUnitofWork
    {
        private readonly AppDbContext _appDbContext;
        public IPictureRepository pictureRepository { get; set; }
        public IUserRepository userRepository { get; set; } 
        

        public UnitofWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            userRepository = new UserRepository(_appDbContext);
            pictureRepository = new PictureRepository(_appDbContext);
        }

        public async Task Commit()
        {
            await _appDbContext.SaveChangesAsync();
        }
    }
}
