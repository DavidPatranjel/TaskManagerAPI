using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Models.Entities;
using TaskManager.Services.GenericService;

namespace TaskManager.Services.ResponsiblesService
{
    public class ResponsiblesService : GenericService<Responsible>, IResponsiblesService
    {
        public ResponsiblesService(AppDbContext db) : base(db) { }

    }
}
