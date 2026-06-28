using Microsoft.EntityFrameworkCore;

namespace SGE.Infraestructura;
public class UsuarioRepository : IUsuarioRepository
{
    protected readonly SgeContext _context;
    public UsuarioRepository(SgeContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void Agregar(String nombre, string correoElectronico, string contrasenaHasheada, bool esAdministrador, List<PermisoUsuarios> permisoUsuarios)
    {
        var usuario = new Usuario(
            nombre,
            correoElectronico,
            contrasenaHasheada,
            esAdministrador,
            permisoUsuarios
        );
        _context.Usuarios.Add(usuario);
    }

    public void Eliminar(Guid id)
    {
        var usuario = _context.Usuarios.Find(id);
        if (usuario != null)
        {
            _context.Usuarios.Remove(usuario);
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

    public Usuario? ObtenerPorCorreoElectronico(string correoElectronico)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.CorreoElectronico == correoElectronico);
        /*if (usuario != null)
        {
            return usuario;
        }
        throw new Exception("Usuario no encontrado.");*/
        return usuario;
    }

    public List<Usuario> ListarTodos()
    {
        return _context.Usuarios.ToList();
    }

    public bool TienePermiso(Guid usuarioId)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == usuarioId);
        return usuario != null && usuario.EsAdministrador;
    }

    public void ModificarUsuario(string nombre, string correoElectronico, string contrasenaHasheada)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.CorreoElectronico == correoElectronico);
        if (usuario == null)
        {
            throw new Exception("Usuario no encontrado.");
        }
        usuario.ModificarUsuario(nombre, correoElectronico, contrasenaHasheada);
    }

    public void ModificarPermiso(Guid usuarioId, List<PermisoUsuarios> permisosNuevos, bool esAdministrador)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == usuarioId);
        if (usuario == null)
        {
            throw new Exception("Usuario no encontrado.");
        }
        usuario.ModificarPermisos(permisosNuevos);
    }
}