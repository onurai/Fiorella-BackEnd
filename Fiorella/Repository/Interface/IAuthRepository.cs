using Fiorella.Data.Entity;

namespace Fiorella.Repository.Interface
{
    public interface IAuthRepository
    {
        Task<string> GenerateJWTToken(User user);
    }
}
