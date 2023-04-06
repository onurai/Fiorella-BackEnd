using Fiorella.Data.Context;
using Fiorella.Data.Entity;
using Fiorella.Dto;
using Fiorella.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Fiorella.Repository.Implementation
{
    public class UserRepository : EfRepository<User, int>, IUserRepository
    {
        private readonly AppDbContext _appDbContext;

        public UserRepository(AppDbContext dbContext) : base(dbContext)
        {
            _appDbContext = dbContext;
        }

        public async Task<User> Create(UserDto userDto)
        {
            User user = new()
            {
                Firstname = userDto.Firstname,
                Lastname = userDto.Lastname,    
                Username = userDto.Username,
                Password = userDto.Password,
                IsAdmin = userDto.IsAdmin,
                BasketId = userDto.BasketId
            };
            _appDbContext.Users.Add(user);
            await _appDbContext.SaveChangesAsync();
            return user;
        }

        public async Task<List<User>> GetAll()
        {
            List<User> users = await _appDbContext.Users.ToListAsync();
            return users;
        }

        public Task<User> Get(int id)
        {
            var result = _appDbContext.Users.FirstOrDefaultAsync(i => i.Id == id);
            return result;
        }

        public async Task<bool> Remove(int id)
        {
            var result = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            _appDbContext.Users.Remove(result);
            await _appDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Update(int id, UserDto userDto)
        {
            User user = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

            user.Firstname = userDto.Firstname;
            user.Lastname = userDto.Lastname;
            user.Username = userDto.Username;
            user.Password = userDto.Password;

            await _appDbContext.SaveChangesAsync();
            return true;
        }
    }
}
