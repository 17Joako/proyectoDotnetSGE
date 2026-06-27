public class ModificarPermisosUsuarioUseCase(IUsuarioRepository repo, IUnidadDeTrabajo unidadDeTrabajo)
{
    public void Ejecutar(ModificarPermisoRequest request, Guid usuarioId)
    {
        if (!repo.TienePermiso(usuarioId))
        {
            throw new AutorizacionException("El usuario no tiene permiso para modificar permisos.");
        }
        repo.ModificarPermiso(request.IdUsuarioAModificar, request.PermisosNuevos,request.esAdmin);
        unidadDeTrabajo.Guardar(); 
    }
}