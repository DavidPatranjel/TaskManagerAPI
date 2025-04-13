using Microsoft.EntityFrameworkCore;
using TaskManager.Models.Entities;
using Task = TaskManager.Models.Entities.Task;


namespace TaskManager.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Task> Tasks { get; set; }
        public DbSet<Responsible> Responsibles { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Task>()
                .HasOne(t => t.Responsible)
                .WithMany(r => r.Tasks)
                .HasForeignKey(t => t.ResponsibleId);

            builder.Entity<Task>()
                .HasMany(t => t.Comments)
                .WithOne(c => c.Task)
                .HasForeignKey(c => c.TaskId);

            builder.Entity<Task>()
                .HasOne(t => t.ParentTask)
                .WithMany()
                .HasForeignKey(t => t.ParentTaskId);
        }
    }
}
