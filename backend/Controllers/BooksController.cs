using Microsoft.AspNetCore.Mvc;
using backend.Data;

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
}
