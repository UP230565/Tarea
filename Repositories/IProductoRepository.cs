using ProductosApi.Models;

namespace ProductosApi.Repositories;

public interface IProductoRepository
{
    List<Producto> ObtenerTodos();
    Producto? ObtenerPorId(int id);
    int Insertar(Producto producto);
    bool Actualizar(Producto producto);
    bool Eliminar(int id);

    // Método adicional para cumplir el punto "Buscar"
    List<Producto> Buscar(string termino);
}
