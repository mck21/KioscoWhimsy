using System;
using System.Collections.Generic;

namespace Kiosco_Whimsy.Backend.Modelos;

/// <summary>
/// POCO Cliente
/// </summary>
public partial class Cliente
{
    public int Idcliente { get; set; }

    public string Cif { get; set; } = null!;

    public int PersonaId { get; set; }

    public int OfertaId { get; set; }

    public virtual Oferta Oferta { get; set; } = null!;

    public virtual Persona Persona { get; set; } = null!;

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
