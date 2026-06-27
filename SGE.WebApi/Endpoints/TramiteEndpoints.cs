using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace SGE.WebApi.Endpoints.TramiteEndpoints;
public static class TramiteEndpoints
{
    public static void MapTramiteEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/tramites")
            .WithTags("Trámites");

        group.MapGet("/listarExpediente", ListarTramitesPorExpediente);
        group.MapGet("/listar", ListarTramites);
        group.MapPut("/ModificarTramite", ModificarTramite).RequireAuthorization();
        group.MapDelete("/EliminarTramite", TramiteBaja).RequireAuthorization();
        group.MapPost("/AgregarTramite", TramiteAlta).RequireAuthorization();
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
        ClaimsPrincipal User,
        ModificarTramiteUseCase useCase)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userId = Guid.Parse(userIdClaim!);
        var dto = new ModificarTramiteRequest(request.id,request.nuevoContenido, request.nuevaEtiqueta, request.nuevoExpedienteId);
        useCase.Ejecutar(dto,userId);
        return Results.Ok(new { mensaje = "Trámite modificado" });
    }
    private static IResult TramiteBaja(
        EliminarTramiteRequest request,
        ClaimsPrincipal User,
        TramiteBajaUseCase useCase)
    {
         var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userId = Guid.Parse(userIdClaim!);
        var dto = new EliminarTramiteRequest(request.Id);
        useCase.Ejecutar(dto,userId);
        return Results.Ok(new { mensaje = "Trámite eliminado" });
    }
    private static IResult TramiteAlta(
        AgregarTramiteRequest request,
        ClaimsPrincipal User,
        TramiteAltaUseCase useCase)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userId = Guid.Parse(userIdClaim!);
        var dto = new AgregarTramiteRequest(request.ExpedienteID, request.Contenido);
        useCase.Ejecutar(dto,userId);
        return Results.Ok(new { mensaje = "Trámite agregado" });
    }
}
