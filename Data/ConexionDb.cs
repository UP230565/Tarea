using MySqlConnector;

namespace ProductosApi.Data;

/// <summary>
/// PUNTO 1: CONEXIÓN. Fábrica de conexiones a MySQL.
/// Lee la cadena de conexión desde appsettings.json.
/// </summary>
public class ConexionDb
{
    private readonly string _cadenaConexion;

    public ConexionDb(IConfiguration configuration)
    {
        _cadenaConexion = configuration.GetConnectionString("MySql")
            ?? throw new InvalidOperationException("Falta la cadena de conexión 'MySql' en appsettings.json.");
    }

    /// <summary>Crea (sin abrir) una nueva conexión. Quien la use debe abrirla y liberarla con 'using'.</summary>
    public MySqlConnection CrearConexion() => new MySqlConnection(_cadenaConexion);
}
