using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement
{
    public class EntityOperations
    {
        MyDbContext _db;
        public EntityOperations(MyDbContext context) 
        {
            _db = context;
        }
        public async Task CreateAuthor(Author author)
        {
            if(author == null)
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
                Author author = await _db.authors.FindAsync(authorId);
                if (author != null)
                {
                    if (author.Books == null)
                        author.Books = new Collection<Book>();
                    author.Books.Add(book);
                    book.Author = author;
                    await _db.SaveChangesAsync();
                    Console.WriteLine("Book added Succesfully");
                }
                else
                {
                    Console.WriteLine("No author found");
                }
            }
        }
        
        public async Task ListofBooks()
        {
            var books = _db.books.ToList();
            if (books == null)
            {
                Console.WriteLine("No books");
            }
            else
            {
                foreach (var book in books)
                {
                    Console.WriteLine("Book: " + book.BookId);
                    Console.WriteLine(book.Title);
                    Console.WriteLine(book.YearPublished);
                    Console.WriteLine("Author: " + book.AuthorId);
                    Console.WriteLine(book.Author.Name);
                    Console.WriteLine(book.Author.Email);
                }
            }
        }
        public async Task UpdateBookTitle(int bookId)
        {
            Book book = await _db.books.FindAsync(bookId);
            if(book == null)
            {
                Console.WriteLine("Invalid book");
            }
            else
            {
                Console.WriteLine("Enter title to update: ");
                string title = Console.ReadLine();
                book.Title = title;
                await _db.SaveChangesAsync();
            }
        }
        public async Task DeleteBook(int bookId)
        {
            Book book = await _db.books.FindAsync(bookId);
            if (book == null)
            {
                Console.WriteLine("Invalid book");
            }
            else
            {
                _db.books.Remove(book);
                await _db.SaveChangesAsync();
            }
        }
        public async Task ListofAuthors()
        {
            var authors = _db.authors.ToList();
            if (authors == null)
            {
                Console.WriteLine("No authors");
            }
            else
            {
                foreach (var author in authors)
                {
                    Console.WriteLine("author: " + author.AuthorId);
                    Console.WriteLine(author.Name);
                    Console.WriteLine(author.Email);
                }
            }
        }
        //public async void ListOfBooksByAuthor(int authorId)
        //{
        //    Author author = await _db.authors.FindAsync(authorId);
        //    if (author == null)
        //    {
        //        Console.WriteLine("Invalid Author");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Books of author: "+author.Name);
        //        var books = author.Books;
        //        foreach (var book in books)
        //        {
        //            Console.WriteLine(book.Title);
        //        }
        //    }
        //}
    }
}
