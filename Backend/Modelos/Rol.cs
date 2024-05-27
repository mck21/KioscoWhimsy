using System;
using System.Collections.Generic;

namespace Kiosco_Whimsy.Backend.Modelos;

/// <summary>
/// POCO Rol
/// </summary>
public partial class Rol
{
    public int Idrol { get; set; }

    public string NombreRol { get; set; } = null!;

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();

    public virtual ICollection<Permiso> Permisos { get; set; } = new List<Permiso>();
}
