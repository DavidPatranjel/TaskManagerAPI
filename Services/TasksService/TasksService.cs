using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models.Entities;
using TaskManager.Services.GenericService;

namespace TaskManager.Services.TasksService
{
    public class TasksService : GenericService<Models.Entities.Task>, ITasksService
    {
        public TasksService(AppDbContext db) : base(db) { }
        public async Task<List<Comment>> GetCommentsFromTask(int idtask)
        {
            return await _db.Comments.Where(a => a.TaskId == idtask).ToListAsync();
        }

        public async Task<List<Models.Entities.Task>> GetTasksByResponsibleId(int responsibleId)
        {
            return await _db.Tasks
                .Where(t => t.ResponsibleId == responsibleId)
                .ToListAsync();
        }
    }
}
