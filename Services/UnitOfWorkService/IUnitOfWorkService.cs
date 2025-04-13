using Microsoft.AspNetCore.Identity;
using TaskManager.Models.Entities;
using TaskManager.Services;

namespace TaskManager.Services.UnitOfWorkService
{
    public interface IUnitOfWorkService : IDisposable
    {
        TasksService.ITasksService Tasks { get; }
        CommentsService.ICommentsService Comments { get; }
        ResponsiblesService.IResponsiblesService Responsibles { get; }
        int Save();
    }
}
