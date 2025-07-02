using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIExample.Model
{
    [Table("Trainee")]
    public class Trainee
    {
        [Key]
        public int id {  get; set; }
        [Required]
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
