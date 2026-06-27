using System.Security.Cryptography.X509Certificates;
using SGE.Infraestructura;

public class AutorizacionService: IAutorizacionService
{
    private readonly SgeContext _context;

    public AutorizacionService(SgeContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }        
    public bool PoseeElPermiso(Guid usuarioId, Permiso permiso)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == usuarioId);
        if (usuario == null)
        {
            return false;
        }

        if (usuario.EsAdministrador)
        {
            return true;
        }

        var permisos = usuario.ListaPermisos ?? new List<PermisoUsuarios>();

        if (permiso == Permiso.TramiteBaja)
        {
            return permisos.Contains(PermisoUsuarios.TramiteBaja)
                || permisos.Contains(PermisoUsuarios.ExpedienteBaja);
        }

        return permisos.Contains((PermisoUsuarios)permiso);
    }
}
