using System;
using System.Collections.Generic;

namespace DGA001.Models;

public partial class Vehiculo
{
    public int Id { get; set; }

    public int? DeclaracionId { get; set; }

    public string? Marca { get; set; }

    public string? Modelo { get; set; }

    public int? AnioFabricacion { get; set; }

    public string? TipoVehiculo { get; set; }

    public string? Motor { get; set; }

    public string? Color { get; set; }

    public string? Chasis { get; set; }

    public string? CilindrajeCc { get; set; }

    public string? Estado { get; set; }

    public virtual Declaracion? Declaracion { get; set; }
}
