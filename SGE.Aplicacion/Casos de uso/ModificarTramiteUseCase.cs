public class ModificarTramiteUseCase(ITramiteRepository repo, IAutorizacionService autorizacion, ActualizacionEstadoExpedienteService servicio, IUnidadDeTrabajo unidadDeTrabajo)
{
    public void Ejecutar(ModificarTramiteRequest request)
    {
        if (!autorizacion.PoseeElPermiso(request.usuarioId, Permiso.TramiteModificacion))
        {
            throw new AutorizacionException("El usuario no tiene permiso para modificar trámites.");
        }
        repo.ModificarTramite(request.id, request.nuevoContenido, request.nuevaEtiqueta, request.nuevoExpedienteId, request.usuarioId);
        servicio.ActualizarEstadoExpediente(request.nuevoExpedienteId, request.usuarioId);
        unidadDeTrabajo.Guardar();
    }
}