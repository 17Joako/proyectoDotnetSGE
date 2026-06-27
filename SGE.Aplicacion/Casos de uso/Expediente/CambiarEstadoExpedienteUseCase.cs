public class CambiarEstadoExpedienteUseCase(IExpedienteRepository repo, IAutorizacionService autorizacon, IUnidadDeTrabajo unidadDeTrabajo)
{
    public void Ejecutar(CambiarEstadoRequest request, Guid IdUsuario)
    {
        if (!autorizacon.PoseeElPermiso(IdUsuario, Permiso.ExpedienteModificacion))
        {
            throw new AutorizacionException("El usuario no tiene permiso para modificar expedientes.");
        }
        Expediente E= repo.ObtenerExpedientePorId(request.IdExpediente);
        E.CambiarEstado(request.NuevoEstado, IdUsuario);
        unidadDeTrabajo.Guardar();
    }
}