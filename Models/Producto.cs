using System.ComponentModel.DataAnnotations;

namespace ProductosApi.Models;

/// <summary>
/// Entidad Producto (tabla "productos").
/// </summary>
public class Producto
{
    /// <summary>Identificador (autoincremental, lo asigna la BD).</summary>
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio.")]
    [StringLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [StringLength(255)]
    public string? Descripcion { get; set; }

    [StringLength(50)]
    public string Categoria { get; set; } = "General";

    [Range(0.01, 9999999, ErrorMessage = "El precio debe ser mayor a 0.")]
    public decimal Precio { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo.")]
    public int Stock { get; set; }

    /// <summary>Fecha de registro (la asigna la BD).</summary>
    public DateTime FechaRegistro { get; set; }
}
