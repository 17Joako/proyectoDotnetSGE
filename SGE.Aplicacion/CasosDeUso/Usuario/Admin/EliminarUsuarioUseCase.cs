public class EliminarUsuarioUseCase(IUsuarioRepository _usuarioRepository, IUnidadDeTrabajo unidadDeTrabajo)
{
    public void Ejecutar(EliminarUsuarioRequest request, Guid IdUsuario)
    {
        // Obtener el usuario que se desea eliminar
       
        var usuario = _usuarioRepository.ObtenerPorId(IdUsuario);
        if (usuario == null)
        {
            throw new entidadNoEncontradaException("El administrador no existe.");
        }
        // Validar que el usuario tenga permiso para eliminar usuarios
        if (!usuario.EsAdministrador)
        {
            throw new AutorizacionException("El usuario no tiene permiso para eliminar usuarios.");
        }
        var usuarioAEliminar= _usuarioRepository.ObtenerPorId(request.IdUsuarioAEliminar);
        if (usuarioAEliminar == null)
        {
            throw new entidadNoEncontradaException("El usuario a eliminar no existe en el contexto actual");
        }
        var usuarioId = request.IdUsuarioAEliminar;
        // Eliminar el usuario del repositorio
        _usuarioRepository.Eliminar(usuarioId);
        unidadDeTrabajo.Guardar();
    }
}