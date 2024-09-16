using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UsuariosApi.Data.Dto.Usuario;
using UsuariosApi.Data.Dtos.Usuario;
using UsuariosApi.Models;

namespace UsuariosApi.Services;

public class UsuarioService
{
	private IMapper _mapper;
	private UserManager<Usuario> _userManager;
	private SignInManager<Usuario> _singInManager;

	public UsuarioService(IMapper mapper, UserManager<Usuario> userManager, SignInManager<Usuario> singInManager)
	{
		_mapper = mapper;
		_userManager = userManager;
		_singInManager = singInManager;
	}

	public async Task Cadastrar(CreateUsuarioDto CreateUsuarioDto)
	{
		Usuario usuario = _mapper.Map<Usuario>(CreateUsuarioDto);
		usuario.DataNascimento = CreateUsuarioDto.DataNascimento.ToUniversalTime();
		var resultado = await _userManager.CreateAsync(usuario, CreateUsuarioDto.Password);

		if (!resultado.Succeeded)
		{
			throw new ApplicationException("Falha ao cadastrar o usuário");
		}
	}

	public  async Task Login(LoginUsuarioDto LoginUsuarioDto)
	{
	  var resultado = await _singInManager.PasswordSignInAsync(LoginUsuarioDto.Username,LoginUsuarioDto.Password, false, false);

		if (!resultado.Succeeded)
		{
			throw new ApplicationException("Usuário não autenticado");
		}
	}
}
