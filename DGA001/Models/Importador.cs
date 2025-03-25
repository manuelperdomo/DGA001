using System;
using System.Collections.Generic;

namespace DGA001.Models;

public partial class Importador
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? TipoDocumento { get; set; }

    public string? Rnc { get; set; }

    public string? Regimen { get; set; }

    public virtual ICollection<Declaracion> Declaracions { get; set; } = new List<Declaracion>();

    public virtual ICollection<Importacion> Importacions { get; set; } = new List<Importacion>();
}
