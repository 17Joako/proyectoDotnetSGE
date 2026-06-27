public class ListarUsuariosUseCase(IUsuarioRepository repo)
{
    public void Ejecutar(Guid usuarioId)
    {
        if (!repo.TienePermiso(usuarioId))
        {
            throw new AutorizacionException("El usuario no tiene permiso para listar usuarios.");
        }
        repo.ListarTodos();//preguntar si deveria devolver todos los datos
    }
}