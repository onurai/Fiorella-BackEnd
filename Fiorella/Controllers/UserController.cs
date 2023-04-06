using Fiorella.Data.Entity;
using Fiorella.Dto;
using Fiorella.Dto.Login;
using Fiorella.Repository.Interface;
using Fiorella.UnitofWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Fiorella.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<UserController> _logger;

        public UserController(IUnitofWork unitOfWork, ILogger<UserController> logger)
        {
            _unitofWork = unitOfWork;
            _logger = logger;
        }
        [EnableCors("mycors")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Request accepted at {0}", DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt"));
            var result = await _unitofWork.userRepository.GetAll();
            _logger.LogWarning($"Request Successfully  completed at {DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt")}, and result is {JsonSerializer.Serialize(result)}");
            return Ok(result);
        }
        [EnableCors("mycors")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserDto userDto)
        {
            if (ModelState.IsValid != true)
            {
                _logger.LogError("We met error unhandled at", DateTime.Now);
                return BadRequest(ModelState);
            }
            
            User user = new()
            {
                Firstname = userDto.Firstname,
                Lastname = userDto.Lastname,
                Username = userDto.Username,
                Password = userDto.Password,
                IsAdmin = userDto.IsAdmin,
                BasketId = userDto.BasketId,
            };
            _logger.LogInformation("Request launched and User created at {0}", DateTime.Now);
            await _unitofWork.userRepository.Add(user);
            _logger.LogInformation("User added at {0}", DateTime.Now);
            await _unitofWork.Commit();
            _logger.LogInformation("Request finished and Data was saved at {0}", DateTime.Now);
            return Ok(user);
        }
        [EnableCors("mycors")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UserDto userDto)
        {
            try
            {
                var user = await _unitofWork.userRepository.Find(id);
                _logger.LogInformation($"User got from db with Id of {id}");
               
                user.Username = userDto.Username;
                user.Password = userDto.Password;
                user.Firstname = userDto.Firstname;
                user.Lastname = userDto.Lastname;

                _logger.LogInformation("Request launched at {0}", DateTime.Now);
                await _unitofWork.userRepository.Update(user);
                _logger.LogDebug($"User updated from db with Id of {id}");
                await _unitofWork.Commit();
                _logger.LogInformation($"Request is completed successfully, User with ID of {id} and name of {user.Firstname}");
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occured when deleting the user i-th id of {id}");
                throw ex;
            }
        }
        [EnableCors("mycors")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                var result = await _unitofWork.userRepository.Find(id);
                _logger.LogInformation($"User got from db with Id of {id}");
                await _unitofWork.userRepository.Delete(result);
                _logger.LogDebug($"USer deleted from db with Id of {id}");
                await _unitofWork.Commit();
                _logger.LogInformation($"Request is completed successfully, User with ID of {id} and name of {result.Firstname}");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occured when deleting the user i-th id of {id}");
                throw ex;
            }
        }
        [EnableCors("mycors")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetId(int id)
        {
            var result = await _unitofWork.userRepository.Find(id);
            _logger.LogInformation("User searched was found at {0}", DateTime.Now);
            if (result == null)
            {
                _logger.LogInformation("User was not found at {0}", DateTime.Now);
                return NotFound();
            }
            _logger.LogInformation("User was successfully found at {0}", DateTime.Now);
            return Ok(result);
        }




    }
}
