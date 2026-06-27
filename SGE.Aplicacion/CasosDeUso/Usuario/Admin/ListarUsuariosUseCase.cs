public class ListarUsuariosUseCase(IUsuarioRepository repo)
{
    public ListarUsuarioResponse? Ejecutar(ListarUsuariosRequest request)
    {
        if (!repo.TienePermiso(usuarioId))
        {
            throw new AutorizacionException("El usuario no tiene permiso para listar usuarios.");
        }
        var usuarios = repo.ListarTodos();
        return new ListarUsuarioResponse(usuarios); 
    }
}