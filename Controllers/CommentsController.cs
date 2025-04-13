using Microsoft.AspNetCore.Mvc;
using TaskManager.Models.DTOs;
using TaskManager.Models.Entities;
using TaskManager.Services.UnitOfWorkService;
using TaskManager.Shared;


namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : Controller
    {
        private readonly IUnitOfWorkService _unitOfWork;
        private readonly ILogger<CommentsController> _logger;
       
        public CommentsController(IUnitOfWorkService unitOfWork, ILogger<CommentsController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet("/comments/{task_id}")]
        public async Task<IActionResult> GetComments(int task_id)
        {
            _logger.LogDebug("Running getting comments of a task...");

            // Verify if task id is valid
            var task = await _unitOfWork.Tasks.GetById(task_id);

            if (task == null)
            {
                _logger.LogError(ErrorMessages.dbErrorTaskId);
                return NotFound(ErrorMessages.dbErrorTaskId);
            }

            // Getting comments
            var comms = await _unitOfWork.Tasks.GetCommentsFromTask(task_id);
            return Ok(comms);
            
        }

        [HttpPost("{taskid}")]
        public async Task<IActionResult> AddComments(int taskid, [FromBody] CommentDTO addCommentRequest)
        {
            _logger.LogDebug("Running adding a comment...");

            // Verify if task id is valid
            var task = await _unitOfWork.Tasks.GetById(taskid);
            if (task == null)
            {
                _logger.LogError(ErrorMessages.dbErrorTaskId);
                return NotFound(ErrorMessages.dbErrorTaskId);
            }

            // Creating a new comment
            Comment c = new Comment(addCommentRequest);
            c.TaskId = taskid;
            await _unitOfWork.Comments.Create(c);
            _unitOfWork.Save();
            return Ok(addCommentRequest);
            
        }
    }
}
