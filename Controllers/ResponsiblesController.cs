using Microsoft.AspNetCore.Mvc;
using TaskManager.Models.DTOs;
using TaskManager.Models.Entities;
using TaskManager.Services.UnitOfWorkService;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResponsiblesController : Controller
    {
        private readonly IUnitOfWorkService _unitOfWork;
        private readonly ILogger<ResponsiblesController> _logger;

        public ResponsiblesController(IUnitOfWorkService unitOfWork, ILogger<ResponsiblesController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetResponsibles()
        {
            _logger.LogDebug("Running getting all responsibles ...");
            var listresps = (await _unitOfWork.Responsibles.GetAll());
            return Ok(listresps);
        }

        [HttpPost]
        public async Task<IActionResult> AddResponsible(ResponsibleDTO addResponsibleRequest)
        {
            _logger.LogDebug("Running adding a responsible...");
            Responsible r = new Responsible(addResponsibleRequest);
            await _unitOfWork.Responsibles.Create(r);
            _unitOfWork.Save();
            _logger.LogInformation("Adding responsible to DB...");

            return Ok(addResponsibleRequest);
        }

    }
}
