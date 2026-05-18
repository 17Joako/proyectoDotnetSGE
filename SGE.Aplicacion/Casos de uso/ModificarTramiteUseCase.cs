public class ModificarTramiteUseCase(ITramiteRepository repo, IAutorizacionService autorizacion, ActualizacionEstadoExpedienteService servicio)
{
    public void Ejecutar(TramiteRequest request)
    {
        if (!autorizacion.TienePermiso(request.UsuarioID, Permiso.TramiteModificacion))
        {
            throw new AutorizacionException("El usuario no tiene permiso para modificar trámites.");
        }
       //aca me quede en pausa mi logica fue asi(?)
        repo.ModificarTramite(request.Etiqueta, request.Contenido, request.UsuarioID);
        servicio.ActualizarEstadoExpediente(request.ExpedienteID, request.UsuarioID);
    }
}