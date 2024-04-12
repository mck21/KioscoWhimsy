using System;
using System.Collections.Generic;

namespace Kiosco_Whimsy.Backend.Modelos;

public partial class Detalleventa
{
    public int? Cantidad { get; set; }

    public double? PrecioUnitario { get; set; }

    public int VentaId { get; set; }

    public int ProductoId { get; set; }

    public virtual Producto Producto { get; set; } = null!;

    public virtual Venta Venta { get; set; } = null!;
}
