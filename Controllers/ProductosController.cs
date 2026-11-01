using Microsoft.AspNetCore.Mvc;
using ProductosApi.Models;
using ProductosApi.Repositories;

namespace ProductosApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private readonly IProductoRepository _repo;

    public ProductosController(IProductoRepository repo)
    {
        _repo = repo;
    }

    /// <summary>Obtiene todos los productos.</summary>
    [HttpGet]
    public ActionResult<List<Producto>> ObtenerTodos() => Ok(_repo.ObtenerTodos());

    /// <summary>Obtiene un producto por su Id.</summary>
    [HttpGet("{id:int}")]
    public ActionResult<Producto> ObtenerPorId(int id)
    {
        var producto = _repo.ObtenerPorId(id);
        return producto is null ? NotFound(new { mensaje = $"No existe el producto {id}." }) : Ok(producto);
    }

    /// <summary>Busca productos por nombre, descripción o categoría.</summary>
    [HttpGet("buscar")]
    public ActionResult<List<Producto>> Buscar([FromQuery] string termino)
    {
        if (string.IsNullOrWhiteSpace(termino))
            return BadRequest(new { mensaje = "Indica un término de búsqueda." });

        return Ok(_repo.Buscar(termino));
    }

    /// <summary>Crea un nuevo producto.</summary>
    [HttpPost]
    public ActionResult<Producto> Insertar([FromBody] Producto producto)
    {
        producto.Id = _repo.Insertar(producto);
        var creado = _repo.ObtenerPorId(producto.Id);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = producto.Id }, creado);
    }

    /// <summary>Actualiza un producto existente.</summary>
    [HttpPut("{id:int}")]
    public IActionResult Actualizar(int id, [FromBody] Producto producto)
    {
        producto.Id = id;
        return _repo.Actualizar(producto)
            ? Ok(new { mensaje = "Producto actualizado." })
            : NotFound(new { mensaje = $"No existe el producto {id}." });
    }

    /// <summary>Elimina un producto.</summary>
    [HttpDelete("{id:int}")]
    public IActionResult Eliminar(int id)
    {
        return _repo.Eliminar(id)
            ? Ok(new { mensaje = "Producto eliminado." })
            : NotFound(new { mensaje = $"No existe el producto {id}." });
    }
}
