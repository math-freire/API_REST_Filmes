using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers;

[ApiController]
[Route("[controller]")]  // Quando o usuário enviar uma requisição para /Filme, serei atingido neste controlador devido a esse comando de rota.

public class FilmeController : ControllerBase
{

    private static List<Filme> filmes = new List<Filme>();

    [HttpPost] // Sempre que fizermos uma operação do tipo post para esse controlador com prefixo Filme, iremos adicionar esse filme que recebemos por parâmetro.
    public void AdicionaFilme([FromBody] Filme filme)
    {
        filmes.Add(filme); // Lista de filmes
        Console.WriteLine(filme.Titulo);
        Console.WriteLine(filme.Duracao);
    }
}
