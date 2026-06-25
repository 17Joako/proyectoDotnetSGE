using Microsoft.EntityFrameworkCore;
namespace SGE.Infraestructura;
public class UsuarioRepository : IUsuarioRepository
{
    protected readonly SgeContext _context;
    public UsuarioRepository(SgeContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    public void Agregar(String nombre, string correoElectronico,string salt, string contrasenaHasheada)
    {
        var usuario = new Usuario
        (
            nombre,
            correoElectronico,
            salt,
            contrasenaHasheada,
            false,
            new List<PermisoUsuarios>()
        );
        _context.Usuarios.Add(usuario);
        _context.SaveChanges();
    }
    public void Eliminar(Guid id)
    {
        var usuario = _context.Usuarios.Find(id);
        if (usuario != null)
        {
            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
        }
        else
        {
            throw new Exception("Usuario no encontrado.");
        }
    }
    public void Modificar(Usuario usuario)
    {
        var usuarioExistente = _context.Usuarios.Find(usuario.Id);
        if (usuarioExistente == null)
        {
            throw new Exception("Usuario no encontrado.");
        }
        _context.Usuarios.Update(usuario);
        _context.SaveChanges();//revisar
    }
    public Usuario ObtenerPorId(Guid id)
    {
        var usuario = _context.Usuarios.Find(id);
        if (usuario == null)
        {
            throw new Exception("Usuario no encontrado.");
        }
        return usuario;
    }
    public Usuario ObtenerPorCorreoElectronico(string correoElectronico)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.CorreoElectronico == correoElectronico);
        if (usuario == null)
        {
            throw new Exception("Usuario no encontrado.");
        }
        return usuario;
    }
    public List<Usuario> ListarTodos()
    {
        List<Usuario> usuarios = _context.Usuarios.ToList();
        return usuarios;
    }

    public bool TienePermiso(Guid usuarioId)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == usuarioId);
        if(usuario != null && usuario.EsAdministrador)
        {
            return true;
        }
        return false;
    }
    public void ModificarUsuario(string nombre, string correoElectronico, string contrasenaHasheada)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.CorreoElectronico == correoElectronico);
        if (usuario == null)
        {
            throw new Exception("Usuario no encontrado.");
        }
        usuario.ModificarUsuario(nombre, correoElectronico, contrasenaHasheada);

        _context.SaveChanges();
    }

    public void ModificarPermiso(Guid usuarioId, List<PermisoUsuarios> permisosNuevos)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == usuarioId);
        if (usuario == null)
        {
            throw new Exception("Usuario no encontrado.");
        }
        usuario.ModificarPermisos(permisosNuevos);
        _context.SaveChanges();
    }
}