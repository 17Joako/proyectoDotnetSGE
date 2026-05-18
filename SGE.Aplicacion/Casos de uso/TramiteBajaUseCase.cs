public class TramiteBajaUseCase(ITramiteRepository repo, IAutorizacionService autorizacion, ActualizacionEstadoExpedienteService servicio)
{
    public void Ejecutar(TramiteRequest request)
    {
        if (!autorizacion.TienePermiso(request.UsuarioID, Permiso.TramiteBaja))
        {
            throw new AutorizacionException("El usuario no tiene permiso para eliminar trámites.");
        }
        repo.EliminarTramite(request.Id);
        servicio.ActualizarEstadoExpediente(request.ExpedienteID, request.UsuarioID);
    }
}