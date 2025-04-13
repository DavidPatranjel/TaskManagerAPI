using TaskManager.Data;
using TaskManager.Models.Entities;
using TaskManager.Services.GenericService;

namespace TaskManager.Services.CommentsService
{
    public class CommentsService : GenericService<Comment>, ICommentsService
    {
        public CommentsService(AppDbContext db) : base(db) { }

    }
}
