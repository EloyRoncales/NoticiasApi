using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoticiasAPI.Data;
using NoticiasAPI.Models;

namespace NoticiasAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NoticiasController : ControllerBase
{
    private readonly AppDbContext _context;

    // Constructor
    public NoticiasController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/noticias
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Noticia>>> GetNoticias()
    {
        return await _context.Noticias.ToListAsync();
    }

    // GET: api/noticias/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Noticia>> GetNoticia(int id)
    {
        var noticia = await _context.Noticias.FindAsync(id);

        if (noticia == null)
        {
            return NotFound();
        }

        return noticia;
    }

    // POST: api/noticias
    [HttpPost]
    public async Task<ActionResult<Noticia>> PostNoticia(Noticia noticia)
    {
        _context.Noticias.Add(noticia);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetNoticia), new { id = noticia.Id }, noticia);
    }

    // PUT: api/noticias/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutNoticia(int id, Noticia noticia)
    {
        if (id != noticia.Id)
        {
            return BadRequest();
        }

        _context.Entry(noticia).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Noticias.Any(e => e.Id == id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/noticias/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNoticia(int id)
    {
        var noticia = await _context.Noticias.FindAsync(id);
        if (noticia == null)
        {
            return NotFound();
        }

        _context.Noticias.Remove(noticia);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}