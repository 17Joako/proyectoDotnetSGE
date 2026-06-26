namespace SGE.WebApi.Endpoints.UsuariosEndpoint;

public static class UsuarioEndpoints
{
    public static void MapUsuarioEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/usuarios")
            .WithTags("Usuarios");

        group.MapPost("/", RegistrarUsuario);
        group.MapGet("/", ListarUsuarios);
        group.MapPut("/ModificarUsuario", ModificarUsuario);
        group.MapDelete("/EliminarUsuario", EliminarUsuario);
        group.MapPost("/login", Login);
    }

    private static IResult ModificarPermisosUsuario(
        ModificarPermisoRequest request,
        ModificarPermisosUsuarioUseCase useCase)
    {
        var dto = new ModificarPermisoRequest(request.UsuarioId, request.IdUsuarioAModificar, request.ListaPermisos);
        useCase.Ejecutar(dto);

        return Results.Ok(new { mensaje = "Permisos modificados" });
    }
    private static IResult EliminarUsuario(
        EliminarUsuarioRequest request,
        EliminarUsuarioUseCase useCase)
    {
        var dto = new EliminarUsuarioRequest(request.UsuarioId, request.IdUsuarioAEliminar);
        useCase.Ejecutar(dto);

        return Results.Ok(new { mensaje = "Usuario eliminado" });
    }
    private static IResult Login(
        LoginRequest request, 
        LoginUseCase useCase)
    {
        var dto = new LoginRequest(request.CorreoElectronico, request.Contrasena);
        var token = useCase.Ejecutar(dto);
        
        return Results.Ok(new { token });
    }
    private static IResult ModificarUsuario(
        ModificarUsuarioRequest request,
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
            new List<PermisoUsuarios>());

        useCase.Ejecutar(dto);

        return Results.Ok(new { mensaje = "Usuario registrado" });
    }

    private static IResult ListarUsuarios(ListarUsuariosUseCase useCase)
    {
        var usuarios = useCase.Ejecutar();//esto no lo tengo del todo claro
        return Results.Ok(usuarios);
    }


}

