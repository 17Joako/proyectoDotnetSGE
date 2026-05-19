public class ModificarCaratulaExpedienteUseCase(
    IExpedienteRepository expedienteRepository,
    IAutorizacionService autorizacionService
)
{
    public void Ejecutar(ModificarCaratulaRequest request)
    {
        if (!autorizacionService.PoseeElPermiso(request.UsuarioId, Permiso.ExpedienteModificacion))
        {
            throw new AutorizacionException("El usuario no tiene permiso para modificar expedientes.");
        }
        var expediente = expedienteRepository.ObtenerPorId(request.Id);
        expediente.ModificarCaratula(request.Caratula, request.UsuarioId, request.FechaDeCambio);
    }
}