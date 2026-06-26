public class ObtenerExpedientePorIdUseCase(IExpedienteRepository expedienteRepo, ITramiteRepository tramiteRepo)
{
    public ObtenerExpedientePorIdResponse Ejecutar(ObtenerExpedientePorIdRequest request)
    {
        var expediente = expedienteRepo.ObtenerExpedientePorId(request.IdExpediente);
        if (expediente == null)
        {
            throw new Exception("No se encontró el expediente con el ID proporcionado.");
        }

        var tramites = tramiteRepo.ObtenerPorExpedienteId(request.IdExpediente);
        return new ObtenerExpedientePorIdResponse(expediente, tramites);
    }
}