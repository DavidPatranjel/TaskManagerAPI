using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models.DTOs
{
    public class TaskStatusDTO
    {
        public enum TaskStatus
        {
            NotStarted,
            InProgress,
            Completed
        }

        public TaskStatus Status { get; set; } 

    }
    public class TaskDTO : TaskStatusDTO
    {
        [Required(ErrorMessage = "Please insert the task title")]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please insert the description of this task")]
        [MaxLength(400, ErrorMessage = "The task description must have at most 400 characters")]
        public string Description { get; set; } = string.Empty;
        public DateTime? DueDate { get; set; }

        [Required(ErrorMessage = "Please insert the status of this task")]
        public TaskStatus Status { get; set; } = TaskStatus.NotStarted;
        [Required(ErrorMessage = "Please insert the responsible of this task")]
        public int ResponsibleId { get; set; }

        public int? ParentTaskId { get; set; }

        public TaskDTO(Models.Entities.Task task)
        {
            Title = task.Title;
            Description = task.Description;
            Status = (TaskStatus)task.Status;
            DueDate = task.DueDate;
            ResponsibleId = task.ResponsibleId;
            ParentTaskId = task.ParentTaskId;
        }

        public TaskDTO() { }
    }
}
