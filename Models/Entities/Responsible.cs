using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TaskManager.Models.DTOs;

namespace TaskManager.Models.Entities
{
    //Class representing a responsible for one or more tasks

    public class Responsible
    {
        [Key]
        public int ResponsibleId { get; set; }

        [Required(ErrorMessage = "Please insert the name of this responsible")]
        [MaxLength(100, ErrorMessage = "The name of the responsible must have at most 100 characters")]
        public string Name { get; set; } = string.Empty;
        [JsonIgnore]
        public List<Task> Tasks { get; set; } = new();


        public Responsible(ResponsibleDTO rdto)
        {
            Name = rdto.Name;
        }
        public Responsible() { }
    }
}
