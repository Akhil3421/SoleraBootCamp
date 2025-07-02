using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement
{
    [Table("Book")]
    public class Book
    {
        [Key]
        public int BookId {  get; set; }
        public string Title { get; set; }
        public int YearPublished {  get; set; }
        [ForeignKey("Author")]
        public int AuthorId {  get; set; }
        public virtual Author Author { get; set; }
    }
}
