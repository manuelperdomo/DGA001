using System;
using System.Collections.Generic;

namespace DGA001.Models;

public partial class Impuesto
{
    public int Id { get; set; }

    public int? DeclaracionId { get; set; }

    public decimal? PrecioAlPorMenor { get; set; }

    public decimal? FobUnit { get; set; }

    public decimal? ValorFob { get; set; }

    public decimal? Flete { get; set; }

    public decimal? Seguro { get; set; }

    public decimal? Otros { get; set; }

    public decimal? Vcifbruto { get; set; }

    public decimal? Vcifneto { get; set; }

    public decimal? Gravamen { get; set; }

    public decimal? Selectivo { get; set; }

    public decimal? Itbis { get; set; }

    public decimal? TotalApagar { get; set; }

    public virtual Declaracion? Declaracion { get; set; }
}
