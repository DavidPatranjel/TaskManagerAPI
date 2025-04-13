using System.ComponentModel.DataAnnotations;
using TaskManager.Models.Entities;

namespace TaskManager.Models.DTOs
{
    public class CommentDTO
    {
        [Required(ErrorMessage = "Please insert the comment content")]
        [MaxLength(400, ErrorMessage = "The comment content must have at most 400 characters")]
        public string Content { get; set; } = string.Empty;

        public CommentDTO(Comment comm)
        {
            Content = comm.Content;
        }
        public CommentDTO() { }
    }
}
