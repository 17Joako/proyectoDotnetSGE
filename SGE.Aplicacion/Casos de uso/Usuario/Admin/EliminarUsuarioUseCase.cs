public class EliminarUsuarioUseCase(IUsuarioRepository _usuarioRepository)
{
    public void Ejecutar(EliminarUsuarioRequest request)
    {
        // Obtener el usuario que se desea eliminar
       
        var usuario = _usuarioRepository.ObtenerPorId(request.UsuarioId);
        if (usuario == null)
        {
            throw new NegocioException("El administrdor no existe.");
        }
        // Validar que el usuario tenga permiso para eliminar usuarios
        if (!usuario.EsAdministrador)
        {
            throw new AutorizacionException("El usuario no tiene permiso para eliminar usuarios.");
        }        
        var usuarioId = request.IdUsuarioAEliminar;
        // Eliminar el usuario del repositorio
        _usuarioRepository.Eliminar(usuarioId);
    }
}