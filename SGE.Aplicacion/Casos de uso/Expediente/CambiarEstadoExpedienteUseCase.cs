public class CambiarEstadoExpedienteUseCase(IExpedienteRepository repo, IAutorizacionService autorizacon, IUnidadDeTrabajo unidadDeTrabajo)
{
    public void Ejecutar(CambiarEstadoRequest request)
    {
        if (!autorizacon.PoseeElPermiso(request.IdUsuario, Permiso.ExpedienteModificacion))
        {
            throw new AutorizacionException("El usuario no tiene permiso para modificar expedientes.");
        }
        Expediente E= repo.ObtenerExpedientePorId(request.IdExpediente);
        E.CambiarEstado(request.NuevoEstado, request.IdUsuario);
        unidadDeTrabajo.Guardar();
    }
}