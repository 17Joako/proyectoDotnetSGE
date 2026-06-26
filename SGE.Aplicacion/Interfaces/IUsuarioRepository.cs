public interface IUsuarioRepository
{
    void Agregar(String nombre, string correoElectronico, string salt ,string contrasena);
    void Eliminar(Guid id);
    void Modificar(Usuario usuario);

    Usuario ObtenerPorId(Guid id);
        
    Usuario ObtenerPorCorreoElectronico(string correoElectronico);
    
    List<Usuario>? ListarTodos();
    bool TienePermiso(Guid usuarioId);

    void ModificarUsuario(string nombre, string correoElectronico, string contrasena);

    void ModificarPermiso(Guid usuarioId, List<PermisoUsuarios> permisos);
}