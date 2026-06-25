public class ModificarCaratulaExpedienteUseCase(
    IExpedienteRepository expedienteRepository,
    IAutorizacionService autorizacionService,
    IUnidadDeTrabajo unidadDeTrabajo
)
{
    public void Ejecutar(ModificarCaratulaRequest request)
    {
        if (!autorizacionService.PoseeElPermiso(request.IdUsuario, Permiso.ExpedienteModificacion))
        {
            throw new AutorizacionException("El usuario no tiene permiso para modificar expedientes.");
        }
        var expediente = expedienteRepository.ObtenerExpedientePorId(request.IdExpediente);
        expediente.ModificarCaratula(request.Caratula, request.IdUsuario);
        unidadDeTrabajo.Guardar();
    }
}