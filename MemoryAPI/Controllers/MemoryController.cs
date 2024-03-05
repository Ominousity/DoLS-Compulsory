using Domain;
using MemoryRepository;
using Microsoft.AspNetCore.Mvc;

namespace MemoryAPI.Controllers
{
    [ApiController]
    public class MemoryController : ControllerBase
    {
        private readonly IMemoryRepository _memoryRepository;
        private readonly ILogger<MemoryController> _logger;
        public MemoryController(ILogger<MemoryController> logger, IMemoryRepository memoryRepository)
        {
            _logger = logger;
            _memoryRepository = memoryRepository;
        }

        [HttpGet]
        public IActionResult GetCalculations(Guid UserId)
        {
            try
            {
                _logger.LogInformation($"Request for getting calculations from user : {UserId}");
                List<Calculation> calculations = _memoryRepository.GetCalculations(UserId);
                return Ok(calculations);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult SaveCalculation(Calculation calculation)
        {
            try
            {
                _logger.LogInformation($"Requst for saving a new calculation");
                _memoryRepository.SaveCalculation(calculation);
                return Ok();
            } catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
