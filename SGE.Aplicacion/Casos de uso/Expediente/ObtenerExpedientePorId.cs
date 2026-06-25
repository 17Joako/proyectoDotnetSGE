public class obtenerExpedientePorIdUseCase(IExpedienteRepository expedienteRepo, ITramiteRepository tramiteRepo)
{
    public obtenerExpedientePorIdResponse Ejecutar(obtenerExpedientePorIdRequest request)
    {
        var expediente = expedienteRepo.ObtenerPorId(request.Id);
        if (expediente == null)
        {
            throw new Exception("No se encontró el expediente con el ID proporcionado.");
        }

        var tramites = tramiteRepo.ObtenerPorExpedienteId(request.Id);
        return new obtenerExpedientePorIdResponse(expediente, tramites);
    }
}