using UsuariosApi.Data.Dto.Usuario;
using UsuariosApi.Models;

namespace UsuariosApi.Profiles;

public class UsuarioProfile: AutoMapper.Profile
{
    public UsuarioProfile()
    {
        CreateMap<CreateUsuarioDto,Usuario>();
    }
}
