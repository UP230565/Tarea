using MySqlConnector;
using ProductosApi.Data;
using ProductosApi.Models;

namespace ProductosApi.Repositories;

public class ProductoRepository : IProductoRepository
{
    private readonly ConexionDb _db;

    public ProductoRepository(ConexionDb db)
    {
        _db = db;
    }

    // PUNTO 2: CONSULTAS (todos los registros)
    public List<Producto> ObtenerTodos()
    {
        var lista = new List<Producto>();
        const string sql = @"SELECT id, nombre, descripcion, categoria, precio, stock, fecha_registro FROM productos ORDER BY id";

        using var conn = _db.CrearConexion();
        conn.Open();
        using var cmd = new MySqlCommand(sql, conn);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
            lista.Add(Mapear(reader));

        return lista;
    }

    // PUNTO 2: CONSULTAS (un registro por Id)
    public Producto? ObtenerPorId(int id)
    {
        const string sql = @"SELECT id, nombre, descripcion, categoria, precio, stock, fecha_registro FROM productos WHERE id = @id";

        using var conn = _db.CrearConexion();
        conn.Open();
        using var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@id", id);
        using var reader = cmd.ExecuteReader();

        return reader.Read() ? Mapear(reader) : null;
    }

    // PUNTO 3: INSERT (devuelve el Id generado)
    public int Insertar(Producto producto)
    {
        const string sql = @"INSERT INTO productos (nombre, descripcion, categoria, precio, stock)
                             VALUES (@nombre, @descripcion, @categoria, @precio, @stock);
                             SELECT LAST_INSERT_ID();";

        using var conn = _db.CrearConexion();
        conn.Open();
        using var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@nombre", producto.Nombre);
        cmd.Parameters.AddWithValue("@descripcion", (object?)producto.Descripcion ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@categoria", producto.Categoria);
        cmd.Parameters.AddWithValue("@precio", producto.Precio);
        cmd.Parameters.AddWithValue("@stock", producto.Stock);

        return Convert.ToInt32(cmd.ExecuteScalar());
    }

    // PUNTO 4: ACTUALIZAR (true si se modificó una fila)
    public bool Actualizar(Producto producto)
    {
        const string sql = @"UPDATE productos
                             SET nombre = @nombre,
                                 descripcion = @descripcion,
                                 categoria = @categoria,
                                 precio = @precio,
                                 stock = @stock
                             WHERE id = @id";

        using var conn = _db.CrearConexion();
        conn.Open();
        using var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@id", producto.Id);
        cmd.Parameters.AddWithValue("@nombre", producto.Nombre);
        cmd.Parameters.AddWithValue("@descripcion", (object?)producto.Descripcion ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@categoria", producto.Categoria);
        cmd.Parameters.AddWithValue("@precio", producto.Precio);
        cmd.Parameters.AddWithValue("@stock", producto.Stock);

        return cmd.ExecuteNonQuery() > 0;
    }

    // PUNTO 5: ELIMINAR (true si se borró una fila)
    public bool Eliminar(int id)
    {
        const string sql = "DELETE FROM productos WHERE id = @id";

        using var conn = _db.CrearConexion();
        conn.Open();
        using var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@id", id);

        return cmd.ExecuteNonQuery() > 0;
    }

    // PUNTO 6: BUSCAR (por nombre, descripción o categoría)
    public List<Producto> Buscar(string termino)
    {
        var lista = new List<Producto>();
        const string sql = @"SELECT id, nombre, descripcion, categoria, precio, stock, fecha_registro
                             FROM productos
                             WHERE nombre LIKE @t OR descripcion LIKE @t OR categoria LIKE @t
                             ORDER BY nombre";

        using var conn = _db.CrearConexion();
        conn.Open();
        using var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@t", $"%{termino}%");
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
            lista.Add(Mapear(reader));

        return lista;
    }

    // Convierte una fila del DataReader en un objeto Producto
    private static Producto Mapear(MySqlDataReader r) => new()
    {
        Id = r.GetInt32("id"),
        Nombre = r.GetString("nombre"),
        Descripcion = r.IsDBNull(r.GetOrdinal("descripcion")) ? null : r.GetString("descripcion"),
        Categoria = r.GetString("categoria"),
        Precio = r.GetDecimal("precio"),
        Stock = r.GetInt32("stock"),
        FechaRegistro = r.GetDateTime("fecha_registro")
    };
}
