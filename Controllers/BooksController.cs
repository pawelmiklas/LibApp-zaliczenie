using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using LibApp.Models;
using LibApp.ViewModels;
using LibApp.Data;
using Microsoft.EntityFrameworkCore;
using LibApp.Repositories;

namespace LibApp.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context, IBookRepository bookRepository)
        {
            _context = context;
            _bookRepository = bookRepository;
        }

        public IActionResult Index() => View(_bookRepository.GetAllBooks());

        public IActionResult Details(int id) => View(_bookRepository.GetBookById(id));

        public IActionResult Edit(int id)
        {
            var book = _bookRepository.GetBookById(id);

            if (book == null)
            {
                return NotFound();
            }

            return View("BookForm", new BookFormViewModel
            {
                Book = book,
                Genres = _context.Genre.ToList()
            });
        }

        public IActionResult New() => View("BookForm", new BookFormViewModel
        {
            Genres = _context.Genre.ToList()
        });

        [HttpPost]
        public IActionResult Save(Book book)
        {
            if (!ModelState.IsValid)
            {
                return New();
            }

            if (book.Id == 0)
            {
                book.DateAdded = DateTime.Now;
                _bookRepository.AddBook(book);
            }
            else
            {
                _bookRepository.UpdateBook(book);
            }

            try
            {
                _bookRepository.Save();
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e);
            }

            return RedirectToAction("Index", "Books");
        }
    }
}