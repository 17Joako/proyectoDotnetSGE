public class ModificarTramiteUseCase(ITramiteRepository repo, IAutorizacionService autorizacion, ActualizacionEstadoExpedienteService servicio)
{
    public void Ejecutar(ModificarTramiteRequest request)
    {
        if (!autorizacion.PoseeElPermiso(request.UsuarioUltimoCambio, Permiso.TramiteModificacion))
        {
            throw new AutorizacionException("El usuario no tiene permiso para modificar trámites.");
        }
        Tramite tramiteExistente = request.tramite;
       //aca me quede en pausa mi logica fue asi(?)
        repo.ModificarTramite(tramiteExistente);
        servicio.ActualizarEstadoExpediente(tramiteExistente.ExpedienteId, request.UsuarioUltimoCambio);
    }
}