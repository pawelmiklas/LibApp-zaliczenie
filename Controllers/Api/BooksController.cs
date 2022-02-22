using AutoMapper;
using LibApp.Data;
using LibApp.Dtos;
using LibApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using LibApp.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace LibApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BooksController(ApplicationDbContext context, IBookRepository bookRepository, IMapper mapper)
        {
            _context = context;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<BookDto> GetBooks(string query = null)
        {
            var booksQuery = _bookRepository.GetAllBooks().Where(x => x.NumberAvailable > 0).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query))
            {
                booksQuery = booksQuery.Where(b => b.Name.Contains(query));
            }

            return booksQuery.ToList().Select(_mapper.Map<Book, BookDto>);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Owner")]
        public IActionResult RemoveBook(int id)
        {
            _bookRepository.DeleteBook(id);
            _bookRepository.Save();
            return Ok();
        }
    }
}