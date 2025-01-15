using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Data.Dto.Usuario;
using UsuariosApi.Data.Dtos.Usuario;
using UsuariosApi.Models;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers;

[ApiController]
[Route("[Controller]")]
public class UsuarioController: ControllerBase
{
	private UsuarioService _usuarioService;

	public UsuarioController(UsuarioService cadastroService)
	{
		_usuarioService = cadastroService;
	}

	// -> Se tiver o authorize em tudo, os anonimos podem ir no get apenas e ja resolve [AllowAnonymous]
	[HttpPost("cadastro")]
	public async Task<IActionResult> CadastrarUsuario(CreateUsuarioDto CreateUsuarioDto)
	{
		await _usuarioService.Cadastrar(CreateUsuarioDto);
		return Ok("Usuario Cadastrado com sucesso");
	}

	[HttpPost("login")]
	public async Task<IActionResult> Login(LoginUsuarioDto LoginUsuarioDto) 
	{
		var token = await _usuarioService.Login(LoginUsuarioDto);
		return Ok(token);
	}
}
