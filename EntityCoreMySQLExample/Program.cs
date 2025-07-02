using EntityCoreMySQLExample;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();


var services = new ServiceCollection();

services.AddDbContext<MyDbContext>(options =>
    options.UseMySql(
        configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(9, 3, 0))
    ));

services.AddScoped<EntityOperations>();


// Build service provider
var serviceProvider = services.BuildServiceProvider();
using var scope = serviceProvider.CreateScope();
var operations = scope.ServiceProvider.GetRequiredService<EntityOperations>();

// Main interactive loop
while (true)
{
    Console.WriteLine("\n");
    Console.WriteLine("Library Management System");
    Console.WriteLine("1. Create Author");
    Console.WriteLine("2. Add Book to Author");
    Console.WriteLine("3. List All Books");
    Console.WriteLine("4. Update Book Title");
    Console.WriteLine("5. Delete Book");
    Console.WriteLine("6. List All Authors");
    Console.WriteLine("7. List Books by Author");
    Console.WriteLine("8. Exit");
    Console.Write("Select an option (1-8): ");

    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1": await CreateAuthor(); break;
        case "2": await AddBookToAuthor(); break;
        case "3": await operations.ListofBooks(); break;
        case "4": await UpdateBookTitle(); break;
        case "5": await DeleteBook(); break;
        case "6": await operations.ListofAuthors(); break;
        case "7": await ListBooksByAuthor(); break;
        case "8":
            Console.WriteLine("Exiting...");
            return;
        default:
            Console.WriteLine("❌ Invalid option. Please select 1–8.");
            break;
    }
}

// -------------------- Helper Methods --------------------

async Task CreateAuthor()
{
    Console.Write("Enter author name: ");
    var name = Console.ReadLine();

    Console.Write("Enter author email: ");
    var email = Console.ReadLine();

    var author = new Author { Name = name, Email = email };
    await operations.CreateAuthor(author);
    Console.WriteLine(" Author created successfully.");
}

async Task AddBookToAuthor()
{
    Console.Write("Enter author ID: ");
    if (!int.TryParse(Console.ReadLine(), out int authorId))
    {
        Console.WriteLine("Invalid author ID.");
        return;
    }

    Console.Write("Enter book title: ");
    var title = Console.ReadLine();

    Console.Write("Enter year published: ");
    if (!int.TryParse(Console.ReadLine(), out int yearPublished))
    {
        Console.WriteLine("Invalid year.");
        return;
    }

    var book = new Book { Title = title, YearPublished = yearPublished };
    await operations.AddBooktoAuthors(authorId, book);
}

async Task UpdateBookTitle()
{
    Console.Write("Enter book ID: ");
    if (!int.TryParse(Console.ReadLine(), out int bookId))
    {
        Console.WriteLine("Invalid book ID.");
        return;
    }

    await operations.UpdateBookTitle(bookId);
}

async Task DeleteBook()
{
    Console.Write("Enter book ID: ");
    if (!int.TryParse(Console.ReadLine(), out int bookId))
    {
        Console.WriteLine("Invalid book ID.");
        return;
    }

    await operations.DeleteBook(bookId);
}

async Task ListBooksByAuthor()
{
    Console.Write("Enter author ID: ");
    if (!int.TryParse(Console.ReadLine(), out int authorId))
    {
        Console.WriteLine("Invalid author ID.");
        return;
    }

    await operations.ListOfBooksByAuthor(authorId);
}
