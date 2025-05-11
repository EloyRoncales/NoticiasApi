using Microsoft.EntityFrameworkCore;
using NoticiasAPI.Data;
using NoticiasAPI.Models;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// Configura el contexto de base de datos en memoria
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("NoticiasDB"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Habilita Swagger en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

// Seed de datos iniciales
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    SeedDatabase(context);
}

app.Run();

// Método para inicializar datos
void SeedDatabase(AppDbContext context)
{
    var jsonFilePath = Path.Combine(AppContext.BaseDirectory, "Data", "NoticiasBD.json");

    if (File.Exists(jsonFilePath))
    {
        var jsonData = File.ReadAllText(jsonFilePath);

        // Deserializar el JSON
        var noticias = JsonSerializer.Deserialize<List<Noticia>>(jsonData);

        if (noticias != null && !context.Noticias.Any())
        {
            context.Noticias.AddRange(noticias); // Usa AddRange para listas
            context.SaveChanges();
        }
    }
    else
    {
        Console.WriteLine($"Archivo JSON no encontrado en: {jsonFilePath}");
    }
}
