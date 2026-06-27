public class ModificarPermisosUsuarioUseCase(IUsuarioRepository repo)
{
    public void Ejecutar(ModificarPermisoRequest request)
    {
        if (!repo.TienePermiso(request.UsuarioId))
        {
            throw new AutorizacionException("El usuario no tiene permiso para modificar permisos.");
        }
        repo.ModificarPermiso(request.UsuarioId, request.PermisosNuevos,request.EsAdministrador);
    }
}