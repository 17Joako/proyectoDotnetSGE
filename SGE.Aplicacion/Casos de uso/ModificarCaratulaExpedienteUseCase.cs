public class ModificarCaratulaExpedienteUseCase(
    IExpedienteRepository expedienteRepository,
    IAutorizacionService autorizacionService
)
{
    public void Ejecutar(Caratularequest request)
    {
        if (!autorizacionService.TienePermiso(request.UsuarioId, Permiso.ExpedienteModificacion))
        {
            throw new AutorizacionException("El usuario no tiene permiso para modificar expedientes.");
        }

        expedienteRepository.ModificarCaratula(request.Id, request.Caratula);
    }
}