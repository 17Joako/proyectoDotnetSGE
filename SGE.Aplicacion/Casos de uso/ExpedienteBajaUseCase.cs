public class ExpedienteBajaUseCase(IExpedienteRepository repoExpediente, ITramiteRepository repoTramite, IAutorizacionService autorizacion, IUnidadDeTrabajo unidadDeTrabajo)
{
    public void Ejecutar(EliminarExpedienteRequest request, Guid usuarioId)
    {
        if (!autorizacion.PoseeElPermiso(usuarioId, Permiso.ExpedienteBaja))
        {
            throw new AutorizacionException("El usuario no tiene permiso para eliminar expedientes.");
        }
        repoTramite.EliminarTramitesPorExpedienteId(request.Id);
        repoExpediente.EliminarExpediente(request.Id);
        unidadDeTrabajo.Guardar();
    }
}