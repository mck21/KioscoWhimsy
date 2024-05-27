using di.proyecto2023.MVVM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kiosco_Whimsy.Backend.Modelos;

/// <summary>
/// POCO Producto
/// </summary>
public partial class Producto : PropertyChangedDataError
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Idproducto { get; set; }

    [Required(ErrorMessage = "El campo Decripción es obligatorio")]
    [MaxLength(50)]
    [MinLength(1)]
    public string? Descripcion { get; set; }

    [Required(ErrorMessage = "El campo Precio es obligatorio")]
    public double Precio { get; set; }

    [Required(ErrorMessage = "El campo Ubicación es obligatorio")]
    public string? Ubicacion { get; set; }

    [Required(ErrorMessage = "El campo Stock es obligatorio")]
    [Range(1, 200, ErrorMessage = "El valor debe estar entre 1 y 200")]
    public int Stock { get; set; }

    public double? Iva { get; set; }

    [Required(ErrorMessage = "El campo Imagen es obligatorio")]
    public string? Imagen { get; set; }

    public int? OfertaId { get; set; }
    
    public int TipoproductoId { get; set; }

    public virtual ICollection<Detalleventa> Detalleventa { get; set; } = new List<Detalleventa>();

    public virtual Oferta? Oferta { get; set; }

    [Required(ErrorMessage = "El campo Categoría es obligatorio")]
    public virtual Tipoproducto Tipoproducto { get; set; } = null!;
}
