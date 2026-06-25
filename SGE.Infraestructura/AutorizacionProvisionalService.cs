using System.Security.Cryptography.X509Certificates;
using SGE.Infraestructura;

public class AutorizacionService: IAutorizacionService
{
    private readonly SgeContext _context;
    public bool PoseeElPermiso(Guid id, PermisoUsuarios permiso)
    {
        bool ret = false;
        var usuario = _context. Usuarios.FirstOrDefault(u => u.Id == id);
        if (usuario != null && usuario.ListaPermisos.Contains(permiso))
        {
            ret = true;
        }
        return ret;
    }
}