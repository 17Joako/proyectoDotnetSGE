public interface IUsuarioRepository
{
    void Agregar(Usuario usuario);
    void Eliminar(Guid id);
    void Modificar(Usuario usuario);
    Usuario ObtenerPorId(Guid id);
    IEnumerable<Usuario> ObtenerTodos();
    bool ExisteCorreoElectronico(string correoElectronico);
}