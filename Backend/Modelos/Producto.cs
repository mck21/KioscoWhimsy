using System;
using System.Collections.Generic;

namespace Kiosco_Whimsy.Backend.Modelos;

public partial class Producto
{
    public int Idproducto { get; set; }

    public string? Descripcion { get; set; }

    public double Precio { get; set; }

    public string? Ubicacion { get; set; }

    public int Stock { get; set; }

    public double? Iva { get; set; }

    public string? Imagen { get; set; }

    public int? OfertaId { get; set; }

    public int TipoproductoId { get; set; }

    public virtual ICollection<Detalleventa> Detalleventa { get; set; } = new List<Detalleventa>();

    public virtual Oferta? Oferta { get; set; }

    public virtual Tipoproducto Tipoproducto { get; set; } = null!;
}
