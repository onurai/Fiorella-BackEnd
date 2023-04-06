using Fiorella.Data.Context;
using Fiorella.Data.Entity;
using Fiorella.Dto.Login;
using Fiorella.Repository.Implementation;
using Fiorella.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Fiorella.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly AppDbContext _appDbContext;
        

        public AuthController(IAuthRepository authRepository, AppDbContext appDbContext)
        {
            _authRepository = authRepository;
            _appDbContext = appDbContext;
        }

        [HttpPost]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest loginDto)
        {
            var user = _appDbContext.Users.FirstOrDefault(u => u.Username == loginDto.Username && u.Password == loginDto.Password);
            if (user == null)
            {
                return NotFound("Username or Password is incorrect");
            }

            var token = await _authRepository.GenerateJWTToken(user);
            return Ok(new LoginResponse { Token = token, UserId = user.Id, Firstname = user.Firstname });
        }

    }
}
