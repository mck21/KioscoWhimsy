using di.proyecto2023.MVVM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kiosco_Whimsy.Backend.Modelos;

/// <summary>
/// POCO Venta
/// </summary>
public partial class Venta : PropertyChangedDataError
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Idventa { get; set; }

    [Required(ErrorMessage = "El campo Fecha es obligatorio")]
    [Range(typeof(DateTime), "1/1/2023", "")]
    public DateTime Fecha { get; set; }

    public double Total { get; set; }

    public string? Mensaje { get; set; }

    public int ClienteId { get; set; }

    public int UsuarioId { get; set; }

    [Required(ErrorMessage = "El campo Cliente es obligatorio")]
    public virtual Cliente Cliente { get; set; } = null!;

    public virtual ICollection<Detalleventa> Detalleventa { get; set; } = new List<Detalleventa>();

    public virtual Usuario Usuario { get; set; } = null!;
}
