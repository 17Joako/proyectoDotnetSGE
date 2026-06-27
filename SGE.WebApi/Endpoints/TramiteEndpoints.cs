namespace SGE.WebApi.Endpoints.TramiteEndpoints;

public static class TramiteEndpoints
{
    public static void MapTramiteEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/tramites")
            .WithTags("Trámites");

        group.MapGet("/listarExpediente", ListarTramitesPorExpediente);
        group.MapGet("/listar", ListarTramites);
        group.MapPut("/ModificarTramite", ModificarTramite);
        group.MapDelete("/EliminarTramite", TramiteBaja);
        group.MapPost("/AgregarTramite", TramiteAlta);
    }
    private static IResult ListarTramitesPorExpediente(
        TramitesPorExpedienteRequest request,
        ListarTramitesPorExpedienteUseCase useCase)
    {
        var dto = new TramitesPorExpedienteRequest(request.IdExpediente);
        var tramites = useCase.Ejecutar(dto);
        return Results.Ok(tramites);
    }
    private static IResult ListarTramites(
        ListarTramitesUseCase useCase)
    {
        var tramites = useCase.Ejecutar();
        return Results.Ok(tramites);
    }
    private static IResult ModificarTramite(
        ModificarTramiteRequest request,
        ModificarTramiteUseCase useCase)
    {
        var dto = new ModificarTramiteRequest(request.id, request.nuevoContenido, request.nuevaEtiqueta, request.nuevoExpedienteId, request.usuarioId);
        useCase.Ejecutar(dto);
        return Results.Ok(new { mensaje = "Trámite modificado" });
    }
    private static IResult TramiteBaja(
        EliminarTramiteRequest request,
        TramiteBajaUseCase useCase)
    {
        var dto = new EliminarTramiteRequest(request.UsuarioID, request.Id);
        useCase.Ejecutar(dto);
        return Results.Ok(new { mensaje = "Trámite eliminado" });
    }
    private static IResult TramiteAlta(
        AgregarTramiteRequest request,
        TramiteAltaUseCase useCase)
    {
        var dto = new AgregarTramiteRequest(request.UsuarioID, request.ExpedienteID, request.Contenido);
        useCase.Ejecutar(dto);
        return Results.Ok(new { mensaje = "Trámite agregado" });
    }
}
