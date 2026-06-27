public class ExpedienteBajaUseCase(IExpedienteRepository repoExpediente, ITramiteRepository repoTramite, IAutorizacionService autorizacion, IUnidadDeTrabajo unidadDeTrabajo)
{
    public void Ejecutar(EliminarExpedienteRequest request, Guid usuarioId)
    {
        if (!autorizacion.PoseeElPermiso(usuarioId, Permiso.ExpedienteBaja))
        {
            throw new AutorizacionException("El usuario no tiene permiso para eliminar expedientes.");
        }
        repoTramite.EliminarTramitesPorExpedienteId(request.IdExpediente);
        repoExpediente.EliminarExpediente(request.IdExpediente);
        unidadDeTrabajo.Guardar();
    }
}