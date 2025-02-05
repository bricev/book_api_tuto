namespace Book_API.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;

[Route("[controller]s")]
[ApiController]
public class BookController(AppDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
    {
        return await context
            .Books
            .Include(book => book.Author)
            .ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Book>> GetBook(int id)
    {
        var book = await context
            .Books
            .Include(book => book.Author)
            .FirstOrDefaultAsync(book => book.Id == id);

        return book == null ? NotFound() : book;
    }

    [HttpPost]
    public async Task<ActionResult<Book>> CreateBook(Book book)
    {
        var author = await context.Authors.FindAsync(book.AuthorId);
        if (author == null) return BadRequest(new {
            message = "Invalid AuthorId: Author does not exist."
        });
        
        book.Author = author;

        context.Books.Add(book);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateBook(int id, Book book)
    {
        if (id != book.Id)
            return BadRequest();

        context.Entry(book).State = EntityState.Modified;
        await context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var book = await context.Books.FindAsync(id);
        if (book == null)
            return NotFound();

        context.Books.Remove(book);
        await context.SaveChangesAsync();

        return NoContent();
    }
}
