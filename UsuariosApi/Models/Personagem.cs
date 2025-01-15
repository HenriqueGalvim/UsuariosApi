using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UsuariosApi.Models;

public class Personagem
{
    [Required]
    [Key]
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Url { get; set; }
    public string Raca { get; set; }
    public int Forca { get; set; }
    public int Vida { get; set; }
}