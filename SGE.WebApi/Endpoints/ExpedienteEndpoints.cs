namespace SGE.WebApi.Endpoints.UsuariosEndpoint;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

public static class ExpedienteEndpoints
{
    public static void MapExpedienteEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/expedientes")
            .WithTags("Expedientes");

        // No requiere autorización
        group.MapPut("/ModificarExpediente", ListarExpedientes);
        group.MapPost("/login", ObtenerExpedientePorId);
        // Requiere autorización
        group.MapPost("/", CambiarEstado).RequireAuthorization(); // resolver que le pasa a esto
        group.MapPost("/", ExpedienteAlta).RequireAuthorization();// x2
        group.MapGet("/", ExpedienteBaja).RequireAuthorization();
        group.MapDelete("/EliminarExpediente", ModificarCaratula).RequireAuthorization();
    }
    private static IResult CambiarEstado(
        CambiarEstadoRequest request,
        CambiarEstadoExpedienteUseCase useCase,
        ClaimsPrincipal user
    )
    {
        var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userId = Guid.Parse(userIdClaim!);
        var dto = new CambiarEstadoRequest(request.IdExpediente, request.NuevoEstado);
        useCase.Ejecutar(dto, userId);

        return Results.Ok(new { mensaje = "Estado cambiado" });
    }
    private static IResult ExpedienteAlta(
        AgregarExpedienteRequest request,
        ExpedienteAltaUseCase useCase,
        ClaimsPrincipal user
    )
    {
        var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userId = Guid.Parse(userIdClaim!);
        var dto = new AgregarExpedienteRequest(request.Caratula, request.FechaCracion);
        useCase.Ejecutar(dto, userId);

        return Results.Ok(new { mensaje = "Expediente agregado" });
    }
    private static IResult ExpedienteBaja(
        EliminarExpedienteRequest request,
        ExpedienteBajaUseCase useCase,
        ClaimsPrincipal user
    )
    {
        var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userId = Guid.Parse(userIdClaim!);
        var dto = new EliminarExpedienteRequest(request.IdExpediente);
        useCase.Ejecutar(dto, userId);

        return Results.Ok(new { mensaje = "Expediente eliminado" });
    }
    private static IResult ModificarCaratula(
        ModificarCaratulaRequest request,
        ModificarCaratulaExpedienteUseCase useCase,
        ClaimsPrincipal user
    )
        
    {
        var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userId = Guid.Parse(userIdClaim!);
        var dto = new ModificarCaratulaRequest(request.IdExpediente, request.Caratula, request.FechaDeCambio);
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

