using Microsoft.EntityFrameworkCore;

namespace SGE.Infraestructura;

public class SgeContext : DbContext
{
    #nullable disable
    public DbSet<Expediente> Expedientes { get; set; }
    public DbSet<Tramite> Tramites { get; set; }

    public DbSet<Usuario> Usuarios { get; set; }
    #nullable restore

    public SgeContext()
    {
    }

    public SgeContext(DbContextOptions<SgeContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=sge.db");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Expediente>(entity =>
        {
            entity.ToTable("Expedientes");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FechaCreacion);
            entity.Property(e => e.FechaUltimaModificacion);
            entity.Property(e => e.UsuarioUltimoCambio);
            entity.Property(e => e.Estado);
            entity.ComplexProperty(c => c.Caratula, caratula =>
            {
                caratula.Property(e => e.Texto).HasColumnName("CaratulaTexto");
            });
        });
        modelBuilder.Entity<Tramite>(entity =>
        {
            entity.ToTable("Tramites");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.ExpedienteId);
            entity.Property(e => e.Etiqueta);
            entity.Property(e => e.FechaCreacion);
            entity.Property(e => e.FechaUltimaModificacion);
            entity.Property(e => e.UsuarioUltimoCambio);
            entity.ComplexProperty(c => c.Contenido, contenido =>
            {
                contenido.Property(e => e.contenido).HasColumnName("contenido");
            });
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("Usuarios");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nombre);
            entity.Property(e => e.ContrasenaHash);
            entity.Property(e => e.CorreoElectronico);
            entity.Property(e => e.EsAdministrador);
            entity.Property(e => e.ListaPermisos)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(p => Enum.Parse<PermisoUsuarios>(p)).ToList()
            );
        });
    }
    public static void Inicializar()
    {
        using var context = new SgeContext();
        if (context.Database.EnsureCreated())
        {
            Console.WriteLine("se creo la base de datos");
            var connection = context.Database.GetDbConnection();
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "PRAGMA journal_mode=DELETE;";
                command.ExecuteNonQuery();
            }
        }
    }
}