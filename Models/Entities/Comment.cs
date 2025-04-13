using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;
using TaskManager.Models.DTOs;
using System.Text.Json.Serialization;

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
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "Please insert the task of this comment")]
        [ForeignKey("Task")]
        public int TaskId { get; set; }
        [JsonIgnore]
        public Task? Task { get; set; }

        public Comment(CommentDTO cdto)
        {
            Content = cdto.Content;
        }
        public Comment() { }
    }
}
