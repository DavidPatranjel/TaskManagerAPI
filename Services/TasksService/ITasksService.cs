using TaskManager.Data;
using TaskManager.Models.Entities;
using TaskManager.Services.GenericService;

namespace TaskManager.Services.TasksService
{
    public interface ITasksService : IGenericService<Models.Entities.Task>
    {
        Task<List<Comment>> GetCommentsFromTask(int idtask);
        Task<List<Models.Entities.Task>> GetTasksByResponsibleId(int responsibleId);

    }
}
