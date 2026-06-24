public class CambiarEstadoExpedienteUseCase(IExpedienteRepository repo, IAutorizacionService autorizacon)
{
    public void Ejecutar(CambiarEstadoRequest request)
    {
        if (!autorizacon.PoseeElPermiso(request.UsuarioId, Permiso.ExpedienteModificacion))
        {
            throw new AutorizacionException("El usuario no tiene permiso para modificar expedientes.");
        }
        Expediente E= repo.ObtenerPorId(request.Id);
        E.CambiarEstado(request.NuevoEstado, request.UsuarioId);
    }
}