using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Data;
using UsuariosApi.Data.Dtos.Personagem;
using UsuariosApi.Models;

namespace UsuariosApi.Controllers;

[ApiController]
[Route("[Controller]")]
[Authorize]
public class PersonagemController: ControllerBase
{
	private UsuarioDbContext _context;
	private IMapper _mapper;
    public PersonagemController(UsuarioDbContext context, IMapper mapper)
    {
		_context = context;
		_mapper = mapper;
	}

	/// <summary>
	/// Adiciona um Personagem no banco de dados
	/// </summary>
	/// <param name="PersonagemDto">Objeto com os campos necessários para criação de um Personagem</param>
	/// <returns>IActionResult</returns>
	/// <response code="201">Caso inserção seja feita com sucesso</response>
	[HttpPost]
	[ProducesResponseType(StatusCodes.Status201Created)]
	public IActionResult AdicionaPersonagem([FromBody] CreatePersonagemDto PersonagemDto)
	{
		Console.WriteLine("Adicionando Personagem");
		Personagem Personagem = _mapper.Map<Personagem>(PersonagemDto);
		_context.Personagens.Add(Personagem);
		_context.SaveChanges();
		return CreatedAtAction(nameof(ListarPersonagemPorId), new { id = Personagem.Id }, Personagem);
	}

	/// <summary>
	/// Lista todos os Personagens.
	/// </summary>
	/// <param name="skip">Número de Personagens a serem ignorados.</param>
	/// <param name="take">Número de Personagens a serem retornados.</param>
	/// <returns>Uma lista de Personagens.</returns>
	/// <response code="200">Retorna uma lista de Personagens.</response>
	[HttpGet]
	public IEnumerable<ReadPersonagemDto> ListarPersonagens([FromQuery] int skip = 0,
		[FromQuery] int take = 50)
	{
		return _mapper.Map<List<ReadPersonagemDto>>(_context.Personagens.Skip(skip).Take(take).ToList());
	}

	/// <summary>
	/// Retorna um Personagem pelo seu ID.
	/// </summary>
	/// <param name="id">ID do Personagem.</param>
	/// <returns>Um Personagem.</returns>
	/// <response code="200">Retorna um Personagem.</response>
	/// <response code="404">Personagem não encontrado.</response>
	[HttpGet("{id}")]
	public IActionResult ListarPersonagemPorId(int id)
	{
		var Personagem = _context.Personagens.FirstOrDefault(personagem => personagem.Id == id);

		if (Personagem == null) return NotFound();

		var PersonagemDto = _mapper.Map<ReadPersonagemDto>(Personagem);
		return Ok(PersonagemDto);
	}

	/// <summary>
	/// Atualiza um Personagem existente.
	/// </summary>
	/// <param name="id">ID do Personagem.</param>
	/// <param name="PersonagemDto">Objeto com os dados do Personagem a serem atualizados.</param>
	/// <returns>IActionResult</returns>
	/// <response code="204">Personagem atualizado com sucesso.</response>
	/// <response code="404">Personagem não encontrado.</response>
	[HttpPut("{id}")]
	public ActionResult AtualizandoPersonagem(int id, [FromBody] UpdatePersonagemDto PersonagemDto)
	{
		var Personagem = _context.Personagens.FirstOrDefault(filme => filme.Id == id)!;
		if (Personagem == null) return NotFound();
		_mapper.Map(PersonagemDto, Personagem);
		_context.SaveChanges();
		return NoContent();
	}

	/// <summary>
	/// Deleta um Personagem.
	/// </summary>
	/// <param name="id">ID do Personagem.</param>
	/// <returns>IActionResult</returns>
	/// <response code="204">Personagem deletado com sucesso.</response>
	/// <response code="404">Personagem não encontrado.</response>
	[HttpDelete("{id}")]
	public ActionResult DeletarPersonagem(int id)
	{
		var Personagem = _context.Personagens.FirstOrDefault(endereco => endereco.Id == id)!;
		if (Personagem == null) return NotFound();

		_context.Remove(Personagem);
		_context.SaveChanges();
		return NoContent();
	}
}
