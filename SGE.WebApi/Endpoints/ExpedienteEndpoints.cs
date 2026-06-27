namespace SGE.WebApi.Endpoints.UsuariosEndpoint;

public static class ExpedienteEndpoints
{
    public static void MapExpedienteEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/expedientes")
            .WithTags("Expedientes");

        group.MapPost("/", CambiarEstado);
        group.MapPost("/", ExpedienteAlta);
        group.MapGet("/", ExpedienteBaja);
        group.MapPut("/ModificarExpediente", ListarExpedientes);
        group.MapDelete("/EliminarExpediente", ModificarCaratula);
        group.MapPost("/login", ObtenerExpedientePorId);
    }
    private static IResult CambiarEstado(
        CambiarEstadoRequest request,
        CambiarEstadoExpedienteUseCase useCase)
    {
        var dto = new CambiarEstadoRequest(request.IdUsuario, request.IdExpediente, request.NuevoEstado);
        useCase.Ejecutar(dto);

        return Results.Ok(new { mensaje = "Estado cambiado" });
    }
    private static IResult ExpedienteAlta(
        AgregarExpedienteRequest request,
        ExpedienteAltaUseCase useCase)
    {
        var dto = new AgregarExpedienteRequest(request.Caratula, request.FechaCracion, request.IdUsuario);
        useCase.Ejecutar(dto, request.IdUsuario);//esto se tiene que modificar OBLIGATORIAMENTE

        return Results.Ok(new { mensaje = "Expediente agregado" });
    }
    private static IResult ExpedienteBaja(
        EliminarExpedienteRequest request,
        ExpedienteBajaUseCase useCase)
    {
        var dto = new EliminarExpedienteRequest(request.IdExpediente);
        useCase.Ejecutar(dto, request.IdExpediente);//esto se tiene que modificar OBLIGATORIAMENTE

        return Results.Ok(new { mensaje = "Expediente eliminado" });
    }
    private static IResult ModificarCaratula(
        ModificarCaratulaRequest request,
        ModificarCaratulaExpedienteUseCase useCase)
    {
        var dto = new ModificarCaratulaRequest(request.IdUsuario, request.IdExpediente, request.Caratula, request.FechaDeCambio);
        useCase.Ejecutar(dto);

        return Results.Ok(new { mensaje = "Carátula modificada" });
    }
    private static IResult ListarExpedientes(
        ListarExpedientesUseCase useCase)
    {
        var expedientes = useCase.Ejecutar();
        return Results.Ok(expedientes);
    }
    private static IResult ObtenerExpedientePorId(
        ObtenerExpedientePorIdRequest request,
        ObtenerExpedientePorIdUseCase useCase)
    {
        var dto = new ObtenerExpedientePorIdRequest(request.IdExpediente);
        var expediente = useCase.Ejecutar(dto);

        return Results.Ok(expediente);
    }
}

