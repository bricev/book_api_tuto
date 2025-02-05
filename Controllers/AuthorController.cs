namespace Book_API.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Models;

[Route("[controller]s")]
[ApiController]
public class AuthorController(AppDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
    {
        return await context.Authors.ToListAsync();
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Author>> GetAuthor(int id)
    {
        var author = await context.Authors.FindAsync(id);
        
        return author == null ? NotFound() : author;
    }

    [HttpGet("{id:int}/books")]
    public async Task<ActionResult<IEnumerable<Book>>> GetAuthorBooks(int id)
    {
        return await context.Books
            .Where(book => book.AuthorId == id)
            .Include(book => book.Author)
            .ToListAsync();
    }
}
