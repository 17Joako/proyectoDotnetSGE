using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace SGE.WebApi.Endpoints.UsuariosEndpoint;

public static class UsuarioEndpoints
{
    public static void MapUsuarioEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/usuarios")
            .WithTags("Usuarios");

        group.MapPost("/login", Login);
        group.MapGet("/listarUsuario", ListarUsuarios).RequireAuthorization();
        group.MapPost("/ModificarPermiso", ModificarPermisosUsuario).RequireAuthorization();
        group.MapPut("/ModificarUsuario", ModificarUsuario).RequireAuthorization();
        group.MapDelete("/EliminarUsuario/{idUsuarioAEliminar}", EliminarUsuario).RequireAuthorization();
        group.MapPost("/", RegistrarUsuario).RequireAuthorization();
    }

    private static IResult ModificarPermisosUsuario(
        ModificarPermisoRequest request,
        ClaimsPrincipal User,
        ModificarPermisosUsuarioUseCase useCase)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userId = Guid.Parse(userIdClaim!);
        var dto = new ModificarPermisoRequest(request.IdUsuarioAModificar, request.PermisosNuevos, request.esAdmin);
        useCase.Ejecutar(dto, userId);
        return Results.Ok(new { mensaje = "Permisos modificados" });
    }

    private static IResult EliminarUsuario(
        Guid idUsuarioAEliminar,
        ClaimsPrincipal User,
        EliminarUsuarioUseCase useCase)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userId = Guid.Parse(userIdClaim!);
        var dto = new EliminarUsuarioRequest(idUsuarioAEliminar);
        useCase.Ejecutar(dto, userId);
        return Results.Ok(new { mensaje = "Usuario eliminado" });
    }

    private static IResult Login(
        LoginRequest request,
        LoginUseCase useCase)
    {
        var dto = new LoginRequest(request.CorreoElectronico, request.Contrasena);
        var loginResponse = useCase.Ejecutar(dto);
        return Results.Ok(new { token = loginResponse.token });
    }

    private static IResult ModificarUsuario(
        ModificarUsuarioRequest request,
        ClaimsPrincipal User,
        ModificarMisDatosUseCase useCase)
    {
        var dto = new ModificarUsuarioRequest(
            request.Nombre,
            request.CorreoElectronico,
            request.Contrasena);
        useCase.Ejecutar(dto);
        return Results.Ok(new { mensaje = "Usuario modificado" });
    }

    private static IResult RegistrarUsuario(
        RegistrarUsuarioRequest request,
        RegistrarUsuarioUseCase useCase)
    {
        var dto = new RegistrarUsuarioRequest(
            request.Nombre,
            request.CorreoElectronico,
            request.Contrasena,
            request.esAdministrador,
            request.permisosUsuario
        );
        useCase.Ejecutar(dto);
        return Results.Ok(new { mensaje = "Usuario registrado" });
    }

    private static IResult ListarUsuarios(
        ClaimsPrincipal User,
        ListarUsuariosUseCase useCase)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userId = Guid.Parse(userIdClaim!);
        var usuarios = useCase.Ejecutar(userId);
        return Results.Ok(usuarios);
    }
}