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
            
            // Seed
            // Admin
            // nombre Juani, correo juani@gmail.com, contraseña admin987
            var contrasenaHash = PasswordHasher.ComputeHash("admin987");
            List<PermisoUsuarios> permisos = new List<PermisoUsuarios>
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

            // Usuario 1
            // nombre Finn, correo finn@gmail.com, contraseña usuario111
            contrasenaHash = PasswordHasher.ComputeHash("usuario111");
            permisos = new List<PermisoUsuarios>
            {
                PermisoUsuarios.TramiteAlta,
                PermisoUsuarios.TramiteBaja,
                PermisoUsuarios.TramiteModificacion
            };
            usuario = new Usuario("Finn", "finn@gmail.com", contrasenaHash, false, permisos);
            context.Usuarios.Add(usuario);
            
            // Usuario 2
            // nombre Lucho, correo lucho@gmail.com, contraseña usuario222
            contrasenaHash = PasswordHasher.ComputeHash("usuario222");
            permisos = new List<PermisoUsuarios>
            {
                PermisoUsuarios.ExpedienteAlta,
                PermisoUsuarios.ExpedienteModificacion,
            };
            usuario = new Usuario("Lucho", "lucho@gmail.com", contrasenaHash, false, permisos);
            context.Usuarios.Add(usuario);
            
            // Usuario 3
            // nombre Joako, correo joako@gmail.com, contraseña usuario333
            contrasenaHash = PasswordHasher.ComputeHash("usuario333");
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
            
            // Usuario 4
            // nombre Bauti, correo bauti@gmail.com, contraseña usuario444
            contrasenaHash = PasswordHasher.ComputeHash("usuario444");
            permisos = new List<PermisoUsuarios>{};
            usuario = new Usuario("Bauti", "bauti@gmail.com", contrasenaHash, false, permisos);
            context.Usuarios.Add(usuario);
            // Fin de la seed
            
            UnidadDeTrabajoRepository udt = new UnidadDeTrabajoRepository(context);
            udt.Guardar();
        }
    }
}