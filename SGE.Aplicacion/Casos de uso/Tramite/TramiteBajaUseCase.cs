public class TramiteBajaUseCase(ITramiteRepository repo, IAutorizacionService autorizacion, ActualizacionEstadoExpedienteService servicio, IUnidadDeTrabajo unidadDeTrabajo)
{
    public void Ejecutar(EliminarTramiteRequest request)
    {
        if (!autorizacion.PoseeElPermiso(request.UsuarioID, Permiso.TramiteBaja))
        {
            throw new AutorizacionException("El usuario no tiene permiso para eliminar trámites.");
        }
        var tramite= repo.ObtenerPorId(request.Id);
        repo.EliminarTramite(request.Id);
        servicio.ActualizarEstadoExpediente(tramite.ExpedienteId, request.UsuarioID);
        unidadDeTrabajo.Guardar();
    }
}