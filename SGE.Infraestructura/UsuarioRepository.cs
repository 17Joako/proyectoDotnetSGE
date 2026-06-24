public class UsuarioRepository : IUsuarioRepository
{
    public UsuarioRepository(SGEContext context)
    {
        _context = context;//esto hay que cambiarlo por una inyeccion de dependencias, para poder usar el contexto de la base de datos
    }

    void Agregar(String nombre, string correoElectronico, string contrasenaHasheada)
    {
        var usuario = new Usuario
        (
            nombre,
            correoElectronico,
            contrasenaHasheada,
            false,
            new List<PermisoUsuarios>()
        );
        _context.Usuarios.Add(usuario);
        _context.SaveChanges();
    }
    void Eliminar(Guid id)
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
    void modificar(Usuario usuario)
    {
        var usuarioExistente = _context.Usuarios.Find(usuario.Id);
        if (usuarioExistente == null)
        {
            throw new Exception("Usuario no encontrado.");
        }
        _context.Usuarios.Update(usuario);
        _context.SaveChanges();//revisar
    }
    Usuario ObtenerPorId(Guid id)
    {
        return _context.Usuarios.Find(id);
    }

    List<Usuario> listarTodos()
    {
        return _context.Usuarios.ToList();
    }
    public bool TienePermiso(int usuarioId)
    {
        var usuario = _context.Usuarios.where(u => u.Id == usuarioId).FirstOrDefault(null);
        if(usuario != null && usuario.EsAdmin)
        {
            return true;
        }
        return false;
    }

    public void ModificarPermiso(int usuarioId, List<string> permisosNuevos)
    {
        var usuario = _context.Usuarios.where(u => u.Id == usuarioId).FirstOrDefault(null);
        if (usuario == null)
        {
            throw new Exception("Usuario no encontrado.");
        }
        usuario.Permisos = permisosNuevos;
        _context.SaveChanges();
    }

    public IEnumerable<Usuario> listarTodos()
    {
        return _context.Usuarios.ToList();
    }
}