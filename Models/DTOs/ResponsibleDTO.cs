using System.ComponentModel.DataAnnotations;
using TaskManager.Models.Entities;

namespace TaskManager.Models.DTOs
{
    public class ResponsibleDTO
    {
        [Required(ErrorMessage = "Please insert the name of this responsible")]
        [MaxLength(100, ErrorMessage = "The name of the responsible must have at most 100 characters")]
        public string Name { get; set; } = string.Empty;

        public ResponsibleDTO(Responsible responsible)
        {
            Name = responsible.Name;
        }
        public ResponsibleDTO() { }

    }
}
