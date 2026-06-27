public class ListarExpedientesUseCase(IExpedienteRepository repo)
{
    public ListarExpedientesResponse Ejecutar()
    {
        var expedientes = repo.BuscarTodos();
        return new ListarExpedientesResponse(expedientes);
    }
}