public class ListarTramitesPorExpedienteUseCase(ITramiteRepository repo)
{
    public ListarTramitesResponse Ejecutar(TramitesPorExpedienteRequest request)
    {
        var tramites = repo.ObtenerPorExpedienteId(request.IdExpediente);
        return new ListarTramitesResponse(tramites);
    }
}