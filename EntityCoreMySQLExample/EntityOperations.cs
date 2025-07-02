using EntityCoreMySQLExample;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace EntityCoreMySQLExample
{
    public class EntityOperations
    {
        private readonly MyDbContext _db;

        public EntityOperations(MyDbContext context)
        {
            _db = context;
        }

        public async Task CreateAuthor(Author author)
        {
            if (author == null)
            {
                Console.WriteLine("Invalid Author");
            }
            else
            {
                _db.authors.Add(author);
                await _db.SaveChangesAsync();
            }
        }

        public async Task AddBooktoAuthors(int authorId, Book book)
        {
            if (book == null)
            {
                Console.WriteLine("Invalid Book");
            }
            else
            {
                var author = await _db.authors.FindAsync(authorId);
                if (author != null)
                {
                    if (author.Books == null)
                        author.Books = new Collection<Book>();
                    author.Books.Add(book);
                    book.Author = author;
                    await _db.SaveChangesAsync();
                    Console.WriteLine("Book added successfully");
                }
                else
                {
                    Console.WriteLine("No author found");
                }
            }
        }

        public async Task ListofBooks()
        {
            var books = await _db.books.Include(b => b.Author).ToListAsync();
            if (books == null || books.Count == 0)
            {
                Console.WriteLine("No books");
            }
            else
            {
                foreach (var book in books)
                {
                    Console.WriteLine($"Book ID: {book.BookId}");
                    Console.WriteLine($"Title: {book.Title}");
                    Console.WriteLine($"Year Published: {book.YearPublished}");
                    Console.WriteLine($"Author ID: {book.AuthorId}");
                    Console.WriteLine($"Author: {book.Author?.Name ?? "Unknown"}");
                    Console.WriteLine($"Author Email: {book.Author?.Email ?? "Unknown"}");
                    Console.WriteLine();
                }
            }
        }

        public async Task UpdateBookTitle(int bookId)
        {
            var book = await _db.books.FindAsync(bookId);
            if (book == null)
            {
                Console.WriteLine("Invalid book");
            }
            else
            {
                Console.Write("Enter title to update: ");
                string title = Console.ReadLine();
                book.Title = title;
                await _db.SaveChangesAsync();
                Console.WriteLine("Book title updated successfully");
            }
        }

        public async Task DeleteBook(int bookId)
        {
            var book = await _db.books.FindAsync(bookId);
            if (book == null)
            {
                Console.WriteLine("Invalid book");
            }
            else
            {
                _db.books.Remove(book);
                await _db.SaveChangesAsync();
                Console.WriteLine("Book deleted successfully");
            }
        }

        public async Task ListofAuthors()
        {
            var authors = await _db.authors.ToListAsync();
            if (authors == null || authors.Count == 0)
            {
                Console.WriteLine("No authors");
            }
            else
            {
                foreach (var author in authors)
                {
                    Console.WriteLine($"Author ID: {author.AuthorId}");
                    Console.WriteLine($"Name: {author.Name}");
                    Console.WriteLine($"Email: {author.Email}");
                    Console.WriteLine();
                }
            }
        }

        public async Task ListOfBooksByAuthor(int authorId)
        {
            var author = await _db.authors.Include(a => a.Books).FirstOrDefaultAsync(a => a.AuthorId == authorId);
            if (author == null)
            {
                Console.WriteLine("Invalid Author");
            }
            else
            {
                Console.WriteLine($"Books by author: {author.Name}");
                if (author.Books == null || author.Books.Count == 0)
                {
                    Console.WriteLine("No books found for this author");
                }
                else
                {
                    foreach (var book in author.Books)
                    {
                        Console.WriteLine($" - {book.Title} ({book.YearPublished})");
                    }
                }
            }
        }
    }
}