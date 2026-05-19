public class ModificarExpedinteUseCase(IExpedienteRepository repo, IAutorizacionService autorizacon)
{
    public void Ejecutar(ModificarExpedienteRequest request)
    {
        if (!autorizacon.PoseeElPermiso(request.UsuarioUltimoCambio, Permiso.ExpedienteModificacion))
        {
            throw new AutorizacionException("El usuario no tiene permiso para modificar expedientes.");
        }
        repo.ModificarExpediente(request.expediente);
    }
}