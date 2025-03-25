using System;
using System.Collections.Generic;

namespace DGA001.Models;

public partial class Importacion
{
    public int Id { get; set; }

    public string NumeroDeclaracion { get; set; } = null!;

    public DateOnly FechaDeclara { get; set; }

    public string? TipoDespacho { get; set; }

    public int? ImportadorId { get; set; }

    public int? TransporteId { get; set; }

    public virtual Importador? Importador { get; set; }

    public virtual Transporte? Transporte { get; set; }
}
