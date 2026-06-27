public class ModificarTramiteUseCase(ITramiteRepository repo, IAutorizacionService autorizacion, ActualizacionEstadoExpedienteService servicio, IUnidadDeTrabajo unidadDeTrabajo)
{
    public void Ejecutar(ModificarTramiteRequest request, Guid usuarioId)
    {
        if (!autorizacion.PoseeElPermiso(usuarioId, Permiso.TramiteModificacion))
        {
            throw new AutorizacionException("El usuario no tiene permiso para modificar trámites.");
        }
        repo.ModificarTramite(request.id, request.nuevoContenido, request.nuevaEtiqueta, request.nuevoExpedienteId, usuarioId);
        servicio.ActualizarEstadoExpediente(request.nuevoExpedienteId, usuarioId);
        unidadDeTrabajo.Guardar();
    }
}