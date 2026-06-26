using System.Security.Cryptography.X509Certificates;
using SGE.Infraestructura;

public class AutorizacionService: IAutorizacionService
{
    private readonly SgeContext _context;
    public AutorizacionService(SgeContext context)
    {
        _context = context;
    }

    public bool PoseeElPermiso(Guid id, Permiso permiso)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);
        return usuario != null && usuario.ListaPermisos.Contains((PermisoUsuarios)permiso);
    }
}
