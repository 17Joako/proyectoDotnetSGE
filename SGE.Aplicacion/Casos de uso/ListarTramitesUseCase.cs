public class ListarTramitesUseCase(ITramiteRepository repo)
{
    public ListarTramitesResponse Ejecutar()
    {
        var tramites = repo.BuscarTodos();
        return new ListarTramitesResponse(tramites);
    }
}