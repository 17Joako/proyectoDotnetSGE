public interface IUsuarioRepository
{
    void Agregar(String nombre, string correoElectronico, string contrasena);
    void Eliminar(Guid id);
    void Modificar(Usuario usuario);
    Usuario ObtenerPorId(Guid id);
    void ObtenerTodos();
    
    Usuario BuscarPersona(string correoElectronico);

    void AgregarPermiso(Guid usuarioId, PermisoUsuarios permiso);
    
    List<PermisoUsuarios> listarTodos();
    bool TienePermiso(Guid usuarioId);

    void ModificarUsuario(string nombre, string correoElectronico, string contrasena);

    void ModificarPermiso(Guid usuarioId, List<PermisoUsuarios> permisos);
}