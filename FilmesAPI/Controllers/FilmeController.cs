using FilmesAPI.Data;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[controller]")]  // Quando o usuário enviar uma requisição para /Filme, serei atingido neste controlador devido a esse comando de rota.

public class FilmeController : ControllerBase
{
    
    private FilmeContext _context;

    public FilmeController(FilmeContext context)
    {
        _context = context;
    }

    [HttpPost] // Sempre que fizermos uma operação do tipo post para esse controlador com prefixo Filme, iremos adicionar esse filme que recebemos por parâmetro.
    public IActionResult AdicionaFilme(
        [FromBody] Filme filme)
    {
        _context.Filmes.Add(filme);
        _context.SaveChanges();
        return CreatedAtAction(nameof(RecuperaFilmePorId),
            new { id = filme.Id },
            filme);
    }


    [HttpGet]
    public IEnumerable<Filme> RecuperaFilmes([FromQuery] int skip = 0,
        [FromQuery] int take = 50)
    {
        return _context.Filmes.Skip(skip).Take(take);
    }


    [HttpGet("{id}")]
    public IActionResult RecuperaFilmePorId(int id)
    {
        var filme = _context.Filmes
            .FirstOrDefault(filme => filme.Id == id);
        if (filme == null) return NotFound();
        return Ok(filme);
    }

}
