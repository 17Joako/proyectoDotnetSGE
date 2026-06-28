public class TramiteAltaUseCase(ITramiteRepository repo, IAutorizacionService autorizacion, ActualizacionEstadoExpedienteService servicio, IUnidadDeTrabajo unidadDeTrabajo)
{
    public void Ejecutar(AgregarTramiteRequest request, Guid usuarioId)
    {
        if (!autorizacion.PoseeElPermiso(usuarioId, Permiso.TramiteAlta))
        {
            throw new AutorizacionException("El usuario no tiene permiso para agregar trámites.");
        }
        Tramite t= new Tramite(request.ExpedienteID, request.Contenido, usuarioId);
        repo.AgregarTramite(t);
        servicio.ActualizarEstadoExpediente(request.ExpedienteID, usuarioId);
        unidadDeTrabajo.Guardar();
    }
}