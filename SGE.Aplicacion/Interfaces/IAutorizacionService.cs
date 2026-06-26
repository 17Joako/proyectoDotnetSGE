public interface IAutorizacionService
{
    bool PoseeElPermiso(Guid usuarioId, Permiso permiso);
}