public class ModificarCaratulaExpedienteUseCase(
    IExpedienteRepository expedienteRepository,
    IAutorizacionService autorizacionService,
    IUnidadDeTrabajo unidadDeTrabajo
)
{
    public void Ejecutar(ModificarCaratulaRequest request, Guid IdUsuario)
    {
        if (!autorizacionService.PoseeElPermiso(IdUsuario, Permiso.ExpedienteModificacion))
        {
            throw new AutorizacionException("El usuario no tiene permiso para modificar expedientes.");
        }
        var expediente = expedienteRepository.ObtenerExpedientePorId(request.IdExpediente);
        expediente.ModificarCaratula(request.Caratula, IdUsuario);
        unidadDeTrabajo.Guardar();
    }
}