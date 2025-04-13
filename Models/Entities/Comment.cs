using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models.Entities
{
    //Class representing a comment added to a task
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        [Required(ErrorMessage = "Please insert the comment content")]
        [MaxLength(400, ErrorMessage = "The comment content must have at most 400 characters")]
        public string Content { get; set; } = string.Empty;

        [Required(ErrorMessage = "The comment time of creation is required")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Please insert the task of this comment")]
        [ForeignKey("Task")]
        public int TaskId { get; set; }

        public required Task Task { get; set; }
    }
}
