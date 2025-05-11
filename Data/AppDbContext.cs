using Microsoft.EntityFrameworkCore;
using NoticiasAPI.Models;

namespace NoticiasAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) 
                : base(options) { }

    public DbSet<Noticia> Noticias { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Noticia>(entity =>
        {
            entity.HasKey(n => n.Id); // Clave primaria
            entity.Property(n => n.Titulo).IsRequired();
            entity.Property(n => n.Contenido).IsRequired();
            entity.Property(n => n.Autor).IsRequired();
            entity.Property(n => n.FechaPublicacion).IsRequired(); // Elimina HasColumnType
        });
    }
}