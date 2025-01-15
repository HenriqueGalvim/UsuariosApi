using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuariosApi.Models;
using UsuariosApi.Data.Dtos.Personagem;

namespace UsuariosApi.Profiles;

public class PersonagemProfile : AutoMapper.Profile
{
    	public PersonagemProfile()
	{

		CreateMap<CreatePersonagemDto, Personagem>();
		CreateMap<UpdatePersonagemDto, Personagem>();
		CreateMap<Personagem, UpdatePersonagemDto>();
		CreateMap<Personagem, ReadPersonagemDto>();
	}
}