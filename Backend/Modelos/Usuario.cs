using System;
using System.Collections.Generic;

namespace Kiosco_Whimsy.Backend.Modelos;

public partial class Usuario
{
    public int Idusuario { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int RolId { get; set; }

    public int? PersonaId { get; set; }

    public virtual Persona? Persona { get; set; }

    public virtual Rol Rol { get; set; } = null!;

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
