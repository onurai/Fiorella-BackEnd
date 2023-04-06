using Fiorella.Data.Entity;
using Fiorella.Dto;

namespace Fiorella.Repository.Interface
{
    public interface IUserRepository : IRepository<User, int>
    {
        Task<List<User>> GetAll();
        Task<User> Get(int id);
        Task<User> Create(UserDto userDto);
        Task<bool> Update(int id,UserDto userDto);
        Task<bool> Remove(int id);
    }
}
