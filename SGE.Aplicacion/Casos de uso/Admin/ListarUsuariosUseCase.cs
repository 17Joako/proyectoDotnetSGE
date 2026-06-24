public class ListarUsuariosUseCase(IUsuarioRepository repo)
{
    public void Ejecutar(ListarUsuariosRequest request)
    {
        if (!repo.TienePermiso(request.UsuarioId))
        {
            throw new AutorizacionException("El usuario no tiene permiso para listar usuarios.");
        }
        repo.listarTodos();//analizar
    }
}