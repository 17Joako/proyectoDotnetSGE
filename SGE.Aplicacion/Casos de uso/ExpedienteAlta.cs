public class ExpedienteAltaUseCase(IExpedienteRepository repositorio, IAutorizacionService autorizacion)
{
    public void Ejecutar(AgregarExpedienteRequest request, Guid usuarioId)
    {
        if (!autorizacion.TienePermiso(usuarioId, Permiso.ExpedienteAlta))
        {
            throw new AutorizacionException("El usuario no tiene permiso para crear expedientes.");
        }
        var expediente = new Expediente(request.Id, request.Caratula, request.FechaCracion, request.FechaUltimaModificacion, request.UsuarioUltimoCambio);
        repositorio.AgregarExpediente(expediente);
    }
}