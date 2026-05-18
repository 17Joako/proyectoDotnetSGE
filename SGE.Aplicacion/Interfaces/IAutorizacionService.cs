public interface IAutorizacionService
{
    bool TienePermiso(Guid usuarioId, Permiso permiso);
}