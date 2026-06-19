public class TramiteAltaUseCase(ITramiteRepository repo, IAutorizacionService autorizacion, ActualizacionEstadoExpedienteService servicio)
{
    public void Ejecutar(TramiteRequest request)
    {
        if (!autorizacion.PoseeElPermiso(request.UsuarioID, Permiso.TramiteAlta))
        {
            throw new AutorizacionException("El usuario no tiene permiso para agregar trámites.");
        }
        Tramite t= new Tramite(request.ExpedienteID, request.Contenido);
        repo.AgregarTramite(t);
        servicio.ActualizarEstadoExpediente(request.ExpedienteID, request.UsuarioID);
    }
}