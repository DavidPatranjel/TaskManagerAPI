using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TaskManager.Models.DTOs;

namespace TaskManager.Models.Entities
{
    //Class representing a task that can be assigned to a responsible 
    public class Task
    {
        [Key]
        public int TaskId { get; set; }

        [Required(ErrorMessage = "Please insert the task title")]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please insert the description of this task")]
        [MaxLength(400, ErrorMessage = "The task description must have at most 400 characters")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please insert the status of this task")]
        public TaskStatus Status { get; set; } = TaskStatus.NotStarted;


        [Required(ErrorMessage = "Please insert the responsible of this task")]
        [ForeignKey("Responsible")]
        public int ResponsibleId { get; set; }
        [JsonIgnore]
        public Responsible Responsible { get; set; }

        [ForeignKey("ParentTask")]
        public int? ParentTaskId { get; set; }
        [JsonIgnore]
        public Task? ParentTask { get; set; }
        [JsonIgnore]
        public List<Comment> Comments { get; set; } = new();

        public enum TaskStatus
        {
            NotStarted,
            InProgress,
            Completed
        }

        public Task(TaskDTO tdto)
        {
            Title = tdto.Title;
            Description = tdto.Description;
            Status = (TaskStatus)tdto.Status;
            ResponsibleId = tdto.ResponsibleId;
            ParentTaskId = tdto.ParentTaskId;
        }

        public Task() { }
    }
}
