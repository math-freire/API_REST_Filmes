using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[controller]")]  // Quando o usuário enviar uma requisição para /Filme, serei atingido neste controlador devido a esse comando de rota.

public class FilmeController : ControllerBase
{
    
    private FilmeContext _context;
    private IMapper _mapper;

    public FilmeController(FilmeContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpPost] // Sempre que fizermos uma operação do tipo post para esse controlador com prefixo Filme, iremos adicionar esse filme que recebemos por parâmetro.
    public IActionResult AdicionaFilme(
        [FromBody] CreateFilmeDto filmeDto)
    {
        Filme filme = _mapper.Map<Filme>(filmeDto);
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
