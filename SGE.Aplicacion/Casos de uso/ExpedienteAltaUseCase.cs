public class ExpedienteAltaUseCase(IExpedienteRepository repositorio, IAutorizacionService autorizacion, IUnidadDeTrabajo unidadDeTrabajo)
{
    public void Ejecutar(AgregarExpedienteRequest request, Guid usuarioId)
    {
        if (!autorizacion.PoseeElPermiso(usuarioId, Permiso.ExpedienteAlta))
        {
            throw new AutorizacionException("El usuario no tiene permiso para crear expedientes.");
        }
        var expediente = new Expediente(request.Caratula, request.FechaCracion);
        repositorio.AgregarExpediente(expediente);
        unidadDeTrabajo.Guardar();
    }
}