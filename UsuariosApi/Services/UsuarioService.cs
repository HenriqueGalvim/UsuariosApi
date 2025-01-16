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
	private TokenService _tokenService;

	public UsuarioService(IMapper mapper, UserManager<Usuario> userManager, SignInManager<Usuario> singInManager, TokenService tokenService)
	{
		_mapper = mapper;
		_userManager = userManager;
		_singInManager = singInManager;
		_tokenService = tokenService;
	}

	public async Task Cadastrar(CreateUsuarioDto CreateUsuarioDto)
	{
		Usuario usuario = _mapper.Map<Usuario>(CreateUsuarioDto);
		usuario.DataNascimento = CreateUsuarioDto.DataNascimento.ToUniversalTime();
		var resultado = await _userManager.CreateAsync(usuario, CreateUsuarioDto.Password);

		if (!resultado.Succeeded)
		{
			var erros = string.Join(", ", resultado.Errors.Select(e => e.Description));
			throw new ApplicationException($"Falha ao cadastrar o usuário: {erros}");
		}
	}

	public async Task<string> Login(LoginUsuarioDto LoginUsuarioDto)
	{
		var resultado = await _singInManager.PasswordSignInAsync(LoginUsuarioDto.Username, LoginUsuarioDto.Password, false, false);

		if (!resultado.Succeeded)
		{
			throw new ApplicationException("Usuário não autenticado");
		}
		var usuario = _singInManager.UserManager.Users.FirstOrDefault(user => user.NormalizedUserName == LoginUsuarioDto.Username.ToUpper())!;

		var token = _tokenService.GenerateToken(usuario);
		
		return token;
	}
}



