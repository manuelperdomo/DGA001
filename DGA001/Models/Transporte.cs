using System;
using System.Collections.Generic;

namespace DGA001.Models;

public partial class Transporte
{
    public int Id { get; set; }

    public int? DeclaracionId { get; set; }

    public string? CodigoPuerto { get; set; }

    public string? Puerto { get; set; }

    public string? PaisProcesoIso { get; set; }

    public string? PaisProceso { get; set; }

    public string? PaisOrigenIso { get; set; }

    public string? PaisOrigen { get; set; }

    public string? Embarcador { get; set; }

    public virtual Declaracion? Declaracion { get; set; }

    public virtual ICollection<Importacion> Importacions { get; set; } = new List<Importacion>();
}
