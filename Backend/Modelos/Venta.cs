using System;
using System.Collections.Generic;

namespace Kiosco_Whimsy.Backend.Modelos;

public partial class Venta
{
    public int Idventa { get; set; }

    public DateTime Fecha { get; set; }

    public double Total { get; set; }

    public string? Mensaje { get; set; }

    public int ClienteId { get; set; }

    public int UsuarioId { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual ICollection<Detalleventa> Detalleventa { get; set; } = new List<Detalleventa>();

    public virtual Usuario Usuario { get; set; } = null!;
}
