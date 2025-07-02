using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement
{
    [Table("Author")]
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }
        public string Name {  get; set; }
        [Required]
        public string Email { get; set; }
        public virtual ICollection<Book> Books { get; set; }
    }
}
