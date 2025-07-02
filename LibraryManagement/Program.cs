using LibraryManagement;

var context = new MyDbContext();
var operations = new EntityOperations(context);

while (true)
{
    Console.WriteLine("\nLibrary Management System");
    Console.WriteLine("1. Create Author");
    Console.WriteLine("2. Add Book to Author");
    Console.WriteLine("3. List All Books");
    Console.WriteLine("4. Update Book Title");
    Console.WriteLine("5. Delete Book");
    Console.WriteLine("6. List of Authors");
    Console.WriteLine("7. Exit");
    Console.Write("Select an option (1-6): ");

    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            await CreateAuthor();
            break;

        case "2":
            await AddBooktoAuthor();
            break;

        case "3":
            await operations.ListofBooks();
            break;

        case "4":
            await UpdateBookTitle();
            break;

        case "5":
            await DeleteBook();
            break;

        case "6":
            await ListOfAuthors();
            break;

        case "7":
            Console.WriteLine("Exiting...");
            return;

        default:
            Console.WriteLine("Invalid option. Please select 1-6.");
            break;
    }

    async Task CreateAuthor()
    {
        Console.Write("Enter author name: ");
        string authorName = Console.ReadLine();
        Console.Write("Enter author email: ");
        string authorEmail = Console.ReadLine();
        var author = new Author { Name = authorName, Email = authorEmail };
        await operations.CreateAuthor(author);
        Console.WriteLine("Created Author succesfully");
    }

    async Task AddBooktoAuthor()
    {
        Console.Write("Enter author ID: ");
        if (int.TryParse(Console.ReadLine(), out int authorId))
        {
            Console.Write("Enter book title: ");
            string bookTitle = Console.ReadLine();
            Console.Write("Enter year published: ");
            if (int.TryParse(Console.ReadLine(), out int yearPublished))
            {
                var book = new Book { Title = bookTitle, YearPublished = yearPublished, AuthorId = authorId };
                await operations.AddBooktoAuthors(authorId, book);
            }
            else
            {
                Console.WriteLine("Invalid year published");
            }
        }
        else
        {
            Console.WriteLine("Invalid author ID");
        }
    }

    async Task UpdateBookTitle()
    {
        Console.Write("Enter book ID to update: ");
        if (int.TryParse(Console.ReadLine(), out int bookId))
        {
            await operations.UpdateBookTitle(bookId);
        }
        else
        {
            Console.WriteLine("Invalid book ID");
        }
    }

    async Task DeleteBook()
    {
        Console.Write("Enter book ID to delete: ");
        if (int.TryParse(Console.ReadLine(), out int deleteBookId))
        {
            await operations.DeleteBook(deleteBookId);
        }
        else
        {
            Console.WriteLine("Invalid book ID");
        }
    }
    async Task ListOfAuthors()
    {
        await operations.ListofAuthors();
    }
}

