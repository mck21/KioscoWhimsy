using System;
using System.Collections.Generic;

namespace Kiosco_Whimsy.Backend.Modelos;

public partial class Permiso
{
    public int Idpermiso { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Rol> Rols { get; set; } = new List<Rol>();
}
