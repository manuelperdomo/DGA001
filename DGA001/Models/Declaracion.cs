using System;
using System.Collections.Generic;

namespace DGA001.Models;

public partial class Declaracion
{
    public int Id { get; set; }

    public DateOnly FechaDeclara { get; set; }

    public DateOnly FechaDesem { get; set; }

    public DateOnly FechaLiquida { get; set; }

    public int Anio { get; set; }

    public string? TipoDespacho { get; set; }

    public string? Colect { get; set; }

    public string? Colecturia { get; set; }

    public string? Declara { get; set; }

    public string? Manifiesto { get; set; }

    public string? ConEmb { get; set; }

    public int? ImportadorId { get; set; }

    public virtual Importador? Importador { get; set; }

    public virtual ICollection<Impuesto> Impuestos { get; set; } = new List<Impuesto>();

    public virtual ICollection<Transporte> Transportes { get; set; } = new List<Transporte>();

    public virtual ICollection<Vehiculo> Vehiculos { get; set; } = new List<Vehiculo>();
}
