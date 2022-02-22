using System.Collections.Generic;
using System.Linq;
using LibApp.Data;
using LibApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LibApp.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddBook(Book book)
        {
            _context.Books.Add(book);
        }

        public void UpdateBook(Book book)
        {
            var updateBook = _context.Books.Find(book.Id);

            updateBook.Name = book.Name;
            updateBook.AuthorName = book.AuthorName;
            updateBook.GenreId = book.GenreId;
            updateBook.ReleaseDate = book.ReleaseDate;
            updateBook.DateAdded = book.DateAdded;
            updateBook.NumberInStock = book.NumberInStock;
        }

        public void DeleteBook(int id)
        {
            _context.Books.Remove(this.GetBookById(id));
        }

        public IEnumerable<Book> GetAllBooks() => _context.Books
                .Include(x => x.Genre)
                .ToList();

        public Book GetBookById(int id) => _context.Books.Include(x => x.Genre).SingleOrDefault(x => x.Id == id);

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}