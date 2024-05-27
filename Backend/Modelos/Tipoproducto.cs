using System;
using System.Collections.Generic;

namespace Kiosco_Whimsy.Backend.Modelos;

/// <summary>
/// POCO Tipoproducto
/// </summary>
public partial class Tipoproducto
{
    public int Idtipoproducto { get; set; }

    public string? Categoria { get; set; }

    public string? Imagen { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
