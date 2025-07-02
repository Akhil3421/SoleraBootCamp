using System.Collections.Generic;

namespace EntityCoreMySQLExample
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
