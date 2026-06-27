public class TramiteBajaUseCase(ITramiteRepository repo, IAutorizacionService autorizacion, ActualizacionEstadoExpedienteService servicio, IUnidadDeTrabajo unidadDeTrabajo)
{
    public void Ejecutar(EliminarTramiteRequest request, Guid usuarioId)
    {
        if (!autorizacion.PoseeElPermiso(usuarioId, Permiso.TramiteBaja))
        {
            throw new AutorizacionException("El usuario no tiene permiso para eliminar trámites.");
        }
        var tramite= repo.ObtenerPorId(request.Id);
        repo.EliminarTramite(request.Id);
        servicio.ActualizarEstadoExpediente(tramite.ExpedienteId, usuarioId);
        unidadDeTrabajo.Guardar();
    }
}