using Microsoft.AspNetCore.Mvc;
using backend.Data;
using backend.Models;

namespace backend.Controllers;

[ApiController]
[Route("api/books")]
public class BooksController : ControllerBase
{
    private readonly AppDbContext _context;
    
    public BooksController(AppDbContext context)
    { _context = context; }

    [HttpGet]
    public IActionResult GetBooks()
    {
        var books = _context.Books.ToList();
        return Ok(books);
    }

    [HttpGet( "{id}" )]
    public IActionResult GetBook(int id)
    {
        var book = _context.Books.Find(id);

        if(book == null)
        {
            return NotFound();
        }
            return Ok(book);
    }

    [HttpPost]
    public IActionResult AddBook([FromBody] Book book)
    {
        book.Id = 0;

        _context.Books.Add(book);
        _context.SaveChanges();
        return StatusCode(201, book);
    }
}