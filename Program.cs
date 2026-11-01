using ProductosApi.Data;
using ProductosApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "API de Productos", Version = "v1" });

    var xml = Path.Combine(AppContext.BaseDirectory, $"{typeof(Program).Assembly.GetName().Name}.xml");
    if (File.Exists(xml)) c.IncludeXmlComments(xml);
});

// Inyección de dependencias
builder.Services.AddSingleton<ConexionDb>();
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();

var app = builder.Build();

// Swagger disponible en la raíz: http://localhost:5000
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API de Productos v1");
    c.RoutePrefix = string.Empty;
});

app.MapControllers();
app.Run();
