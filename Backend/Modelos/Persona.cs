using System;
using System.Collections.Generic;

namespace Kiosco_Whimsy.Backend.Modelos;

/// <summary>
/// POCO Persona
/// </summary>
public partial class Persona
{
    public int Idpersona { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Apellidos { get; set; }

    public string? Direccion { get; set; }

    public string? Poblacion { get; set; }

    public string? CodPostal { get; set; }

    public string? Telefono { get; set; }

    public virtual ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
