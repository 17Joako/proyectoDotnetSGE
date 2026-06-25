public interface IAutorizacionService
{
    bool PoseeElPermiso(Guid usuarioId,PermisoUsuarios permiso);
}