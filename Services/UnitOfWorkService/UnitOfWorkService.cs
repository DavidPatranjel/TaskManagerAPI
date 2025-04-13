using TaskManager.Data;
using TaskManager.Services.ResponsiblesService;

namespace TaskManager.Services.UnitOfWorkService
{
    public class UnitOfWorkService : IUnitOfWorkService
    {
        private readonly AppDbContext db;
        public UnitOfWorkService(AppDbContext db)
        {
            this.db = db;
            Tasks = new TasksService.TasksService(this.db);
            Comments = new CommentsService.CommentsService(this.db);
            Responsibles = new ResponsiblesService.ResponsiblesService(this.db);
        }

        public TasksService.ITasksService Tasks
        {
            get;
            private set;
        }
        public CommentsService.ICommentsService Comments
        {
            get;
            private set;
        }
        public ResponsiblesService.IResponsiblesService Responsibles
        {
            get;
            private set;
        }

        public void Dispose()
        {
            db.Dispose();
        }
        public int Save()
        {
            return db.SaveChanges();
        }
    }
}
