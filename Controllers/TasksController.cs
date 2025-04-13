using Microsoft.AspNetCore.Mvc;
using TaskManager.Models.DTOs;
using TaskManager.Services.UnitOfWorkService;
using TaskManager.Shared;


namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : Controller
    {
        private readonly IUnitOfWorkService _unitOfWork;
        private readonly ILogger<TasksController> _logger;

        public TasksController(IUnitOfWorkService unitOfWork, ILogger<TasksController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            _logger.LogDebug("Running getting all tasks ...");
            var listtasks = (await _unitOfWork.Tasks.GetAll());
            return Ok(listtasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            _logger.LogDebug($"Running getting task with ID {id}");

            // Verify if task id is valid
            var task = await _unitOfWork.Tasks.GetById(id);
            if (task == null)
            {
                _logger.LogError(ErrorMessages.dbErrorTaskId);
                return NotFound(ErrorMessages.dbErrorTaskId);
            }

            return Ok(task);
        }

        [HttpGet("task-responsible/{responsibleId}")]
        public async Task<IActionResult> GetTasksByResponsibleId(int responsibleId)
        {
            _logger.LogDebug($"Running getting tasks for responsible ID {responsibleId}");

            // Verify if task id is valid or if responsible has tasks

            var tasks = await _unitOfWork.Tasks.GetTasksByResponsibleId(responsibleId);
            if (tasks == null || tasks.Count == 0)
            {
                _logger.LogWarning(ErrorMessages.notFoundTasks);
                return NotFound(ErrorMessages.notFoundTasks);
            }

            return Ok(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> AddTask([FromBody] TaskDTO newTask)
        {
            _logger.LogDebug("Running adding a task...");

            // Validate that the status value is within the range
            if (!Enum.IsDefined(typeof(Models.Entities.Task.TaskStatus), newTask.Status))
            {
                _logger.LogError(ErrorMessages.badRequestStatus);
                return BadRequest(ErrorMessages.badRequestStatus);
            }

            // Check if the responsible user exists
            var responsible = await _unitOfWork.Responsibles.GetById(newTask.ResponsibleId);
            if (responsible == null)
            {
                _logger.LogError(ErrorMessages.dbErrorResponsibleId);
                return NotFound(ErrorMessages.dbErrorResponsibleId);
            }

            var t = new Models.Entities.Task(newTask);

            await _unitOfWork.Tasks.Create(t);
            _unitOfWork.Save();

            return Ok(newTask);
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateTaskStatus(int id, [FromBody] TaskStatusDTO statusUpdate)
        {
            _logger.LogDebug("Running update task status...");

            // Validate that the status value is within the range
            int statusValue = (int)statusUpdate.Status;

            if (!Enum.IsDefined(typeof(Models.Entities.Task.TaskStatus), statusValue))
            {
                _logger.LogError(ErrorMessages.badRequestStatus);
                return BadRequest(ErrorMessages.badRequestStatus);
            }

            // Verify if task id is valid
            var task = await _unitOfWork.Tasks.GetById(id);
            if (task == null)
            {
                _logger.LogError(ErrorMessages.dbErrorTaskId);
                return NotFound(ErrorMessages.dbErrorTaskId);
            }

            // Update task status
            task.Status = (Models.Entities.Task.TaskStatus)statusUpdate.Status;
            _unitOfWork.Save();

            return Ok(task);
        }

        [HttpPatch("{id}/parent/{parentId}")]
        public async Task<IActionResult> UpdateParentTask(int id, int parentId)
        {
            _logger.LogDebug("Running update parent task...");

            var task = await _unitOfWork.Tasks.GetById(id);
            var parent = await _unitOfWork.Tasks.GetById(parentId);
            // Verify if task id is valid

            if (task == null || parent == null)
            {
                _logger.LogError(ErrorMessages.dbErrorTaskId);
                return NotFound(ErrorMessages.dbErrorTaskId);
            }

            // Setting parent task
            task.ParentTaskId = parentId;
            task.ParentTask = parent;

            _unitOfWork.Save();

            return Ok(task);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskDTO updatedTaskDto)
        {
            _logger.LogDebug("Running update for a task...");

            // Validate that the status value is within the range
            int statusValue = (int)updatedTaskDto.Status;

            if (!Enum.IsDefined(typeof(Models.Entities.Task.TaskStatus), statusValue))
            {
                _logger.LogError(ErrorMessages.badRequestStatus);
                return BadRequest(ErrorMessages.badRequestStatus);
            }

            // Verify if task id is valid

            var existingTask = await _unitOfWork.Tasks.GetById(id);
            if (existingTask == null)
            {
                _logger.LogError(ErrorMessages.dbErrorTaskId);
                return NotFound(ErrorMessages.dbErrorTaskId);
            }

            // Update fields from the DTO
            existingTask.Title = updatedTaskDto.Title;
            existingTask.Description = updatedTaskDto.Description;
            existingTask.Status = (Models.Entities.Task.TaskStatus)updatedTaskDto.Status;
            existingTask.ResponsibleId = updatedTaskDto.ResponsibleId;
            existingTask.ParentTaskId = updatedTaskDto?.ParentTaskId;


            // Update task
            await _unitOfWork.Tasks.Update(existingTask);
            _unitOfWork.Save();

            return Ok(new TaskDTO(existingTask));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            _logger.LogDebug("Running deleting a task...");

            // Verify if task id is valid
            var task = await _unitOfWork.Tasks.GetById(id);

            if (task == null)
            {
                _logger.LogError(ErrorMessages.dbErrorTaskId);
                return NotFound(ErrorMessages.dbErrorTaskId);
            }

            // Delete operation on all comments
            var listacomms = await _unitOfWork.Tasks.GetCommentsFromTask(id);

            foreach (var comm in listacomms)
            {
                await _unitOfWork.Comments.Delete(comm);
            }

            // Delete the task
            await _unitOfWork.Tasks.Delete(task);
            _unitOfWork.Save();
            return Ok();
        }
    }
}
