using Fiorella.Data.Entity;
using Fiorella.Dto;
using Fiorella.UnitofWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Fiorella.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PictureController : ControllerBase
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ILogger<PictureController> _logger;
        
        public PictureController(IUnitofWork unitOfWork, ILogger<PictureController> logger)
        {
            _unitofWork = unitOfWork;
            _logger = logger;
        }
        [EnableCors("mycors")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Request accepted at {0}", DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt"));
            var result = await _unitofWork.pictureRepository.GetAll();
            _logger.LogWarning($"Request Successfully  completed at {DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt")}, and result is {JsonSerializer.Serialize(result)}");
            return Ok(result);
        }
        [EnableCors("mycors")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PictureDto empDto)
        {
            if (ModelState.IsValid != true)
            {
                _logger.LogError("We met error unhandled at", DateTime.Now);
                return BadRequest(ModelState);
            }

            Picture picture = new()
            {
                Name = empDto.Name,
                Description = empDto.Description,
                Category = empDto.Category,
                Price = empDto.Price,
                Source = empDto.Source,
            };
            _logger.LogInformation("Request launched and Picture created at {0}", DateTime.Now);
            await _unitofWork.pictureRepository.Add(picture);
            _logger.LogInformation("Picture added at {0}", DateTime.Now);
            await _unitofWork.Commit();
            _logger.LogInformation("Request finished and Data was saved at {0}", DateTime.Now);
            return Ok(picture);
        }
        [EnableCors("mycors")]
        [HttpPut ("{id}")]
        public async Task<IActionResult> Update(int id, PictureDto empDto)
        {
            try
            {
                var picture = await _unitofWork.pictureRepository.Find(id);
                _logger.LogInformation($"Picture got from db with Id of {id}");
                picture.Description = empDto.Description;
                picture.Category = empDto.Category;
                picture.Name = empDto.Name;
                picture.Price = empDto.Price;
                picture.Source = empDto.Source;

                _logger.LogInformation("Request launched at {0}", DateTime.Now);
                await _unitofWork.pictureRepository.Update(picture);
                _logger.LogDebug($"Picture updated from db with Id of {id}");
                await _unitofWork.Commit();
                _logger.LogInformation($"Request is completed successfully, Picture with ID of {id} and name of {picture.Name}");
                return Ok(picture);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occured when deleting the picture i-th id of {id}");
                throw ex;
            }
        }
        [EnableCors("mycors")]
        [HttpDelete ("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            try
            {
                var result = await _unitofWork.pictureRepository.Find(id);
                _logger.LogInformation($"Picture got from db with Id of {id}");
                await _unitofWork.pictureRepository.Delete(result);
                _logger.LogDebug($"Picture deleted from db with Id of {id}");
                await _unitofWork.Commit();
                _logger.LogInformation($"Request is completed successfully, Picture with ID of {id} and name of {result.Name}");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occured when deleting the picture i-th id of {id}");
                throw ex;
            }
        }
        [EnableCors("mycors")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetId(int id)
        {
            var result = await _unitofWork.pictureRepository.Find(id);
            _logger.LogInformation("Picture searched was found at {0}", DateTime.Now);
            if (result == null)
            {
                _logger.LogInformation("Picture was not found at {0}", DateTime.Now);
                return NotFound();
            }
            _logger.LogInformation("Picture was successfully found at {0}", DateTime.Now);
            return Ok(result);
        }
    }
}
