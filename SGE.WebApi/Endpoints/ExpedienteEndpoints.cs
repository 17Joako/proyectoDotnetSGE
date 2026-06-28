namespace SGE.WebApi.Endpoints.ExpedienteEndpoints;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

public static class ExpedienteEndpoints
{
    public static void MapExpedienteEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/expedientes")
            .WithTags("Expedientes");

        group.MapPut("/ModificarExpediente", CambiarEstado).RequireAuthorization();
        group.MapPost("/AltaExpediente", ExpedienteAlta).RequireAuthorization();
        group.MapDelete("/BajaExpediente/{idExpediente}", ExpedienteBaja).RequireAuthorization();
        group.MapGet("/ListarExpediente", ListarExpedientes).RequireAuthorization();
        group.MapPost("/ObtenerExpedientePorId", ObtenerExpedientePorId).RequireAuthorization();
        group.MapPut("/ModificarCaratula", ModificarCaratula).RequireAuthorization();
    }

    private static IResult CambiarEstado(
        CambiarEstadoRequest request,
        ClaimsPrincipal User,
        CambiarEstadoExpedienteUseCase useCase)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userId = Guid.Parse(userIdClaim!);
        var dto = new CambiarEstadoRequest(request.IdExpediente, request.NuevoEstado);
        useCase.Ejecutar(dto, userId);
        return Results.Ok(new { mensaje = "Estado cambiado" });
    }

    private static IResult ExpedienteAlta(
        AgregarExpedienteRequest request,
        ClaimsPrincipal User,
        ExpedienteAltaUseCase useCase)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userId = Guid.Parse(userIdClaim!);
        var dto = new AgregarExpedienteRequest(request.Caratula);
        useCase.Ejecutar(dto, userId);
        return Results.Ok(new { mensaje = "Expediente agregado" });
    }

    private static IResult ExpedienteBaja(
        Guid idExpediente,
        ClaimsPrincipal User,
        ExpedienteBajaUseCase useCase)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userId = Guid.Parse(userIdClaim!);
        var dto = new EliminarExpedienteRequest(idExpediente);
        useCase.Ejecutar(dto, userId);
        return Results.Ok(new { mensaje = "Expediente eliminado" });
    }

    private static IResult ModificarCaratula(
        ModificarCaratulaRequest request,
        ClaimsPrincipal User,
        ModificarCaratulaExpedienteUseCase useCase)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userId = Guid.Parse(userIdClaim!);
        var dto = new ModificarCaratulaRequest(request.IdExpediente, request.Caratula);
        useCase.Ejecutar(dto, userId);
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