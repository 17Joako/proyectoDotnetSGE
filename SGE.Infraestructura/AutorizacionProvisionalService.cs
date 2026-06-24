using System.Security.Cryptography.X509Certificates;

public class AutorizacionService: IAutorizacionService
{
    public bool PoseeElPermiso( string permiso, List<string> permisosUsuario)
    {
        if (permisosUsuario == null || permisosUsuario.Count == 0)
        {
            return false;
        }
        return permisosUsuario.Contains(permiso);
    }
}