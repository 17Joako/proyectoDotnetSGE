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
        
        // Crear la BD si no existe
        context.Database.EnsureCreated();
        
        // Verificar si ya existen usuarios
        if (context.Usuarios.Any())
        {
            Console.WriteLine("✓ La base de datos ya tiene datos");
            return;
        }

        Console.WriteLine("📝 Creando datos iniciales...");
        
        var connection = context.Database.GetDbConnection();
        connection.Open();
        using (var command = connection.CreateCommand())
        {
            command.CommandText = "PRAGMA journal_mode=DELETE;";
            command.ExecuteNonQuery();
        }
        
        // Seed - Crear usuarios
        var passwordHasher = new PasswordHasher();
        
        // Admin: Juani
        var contrasenaHash = passwordHasher.Hash("admin987");
        var permisos = new List<PermisoUsuarios>
        {
            PermisoUsuarios.ExpedienteAlta,
            PermisoUsuarios.ExpedienteBaja,
            PermisoUsuarios.ExpedienteModificacion,
            PermisoUsuarios.TramiteAlta,
            PermisoUsuarios.TramiteBaja,
            PermisoUsuarios.TramiteModificacion
        };
        var usuario = new Usuario("Juani", "juani@gmail.com", contrasenaHash, true, permisos);
        context.Usuarios.Add(usuario);

        // Usuario 1: Finn
        contrasenaHash = passwordHasher.Hash("usuario111");
        permisos = new List<PermisoUsuarios>
        {
            PermisoUsuarios.TramiteAlta,
            PermisoUsuarios.TramiteBaja,
            PermisoUsuarios.TramiteModificacion
        };
        usuario = new Usuario("Finn", "finn@gmail.com", contrasenaHash, false, permisos);
        context.Usuarios.Add(usuario);
        
        // Usuario 2: Lucho
        contrasenaHash = passwordHasher.Hash("usuario222");
        permisos = new List<PermisoUsuarios>
        {
            PermisoUsuarios.ExpedienteAlta,
            PermisoUsuarios.ExpedienteModificacion,
        };
        usuario = new Usuario("Lucho", "lucho@gmail.com", contrasenaHash, false, permisos);
        context.Usuarios.Add(usuario);
        
        // Usuario 3: Joako
        contrasenaHash = passwordHasher.Hash("usuario333");
        permisos = new List<PermisoUsuarios>
        {
            PermisoUsuarios.ExpedienteAlta,
            PermisoUsuarios.ExpedienteBaja,
            PermisoUsuarios.ExpedienteModificacion,
            PermisoUsuarios.TramiteAlta,
            PermisoUsuarios.TramiteBaja,
            PermisoUsuarios.TramiteModificacion
        };
        usuario = new Usuario("Joako", "joako@gmail.com", contrasenaHash, false, permisos);
        context.Usuarios.Add(usuario);
        
        // Usuario 4: Bauti
        contrasenaHash = passwordHasher.Hash("usuario444");
        permisos = new List<PermisoUsuarios>{};
        usuario = new Usuario("Bauti", "bauti@gmail.com", contrasenaHash, false, permisos);
        context.Usuarios.Add(usuario);
        
        // Guardar todos los cambios
        context.SaveChanges();
        Console.WriteLine("✓ Datos iniciales creados correctamente");
    }
}