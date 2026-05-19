public class ModificarExpedienteUseCase(IExpedienteRepository repo, IAutorizacionService autorizacon)
{
    public void Ejecutar(ModificarExpedienteRequest request)
    {
        if (!autorizacon.TienePermiso(request.UsuarioUltimoCambio, Permiso.ExpedienteModificacion))
        {
            throw new AutorizacionException("El usuario no tiene permiso para modificar expedientes.");
        }
        repo.ModificarExpediente(request.Id, request.FechaCreacion, request.FechaUltimaModificacion, request.UsuarioUltimoCambio);
    }
}