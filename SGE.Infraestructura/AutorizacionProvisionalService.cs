using System.Security.Cryptography.X509Certificates;

public class AutorizacionProvisionalService: IAutorizacionService
{
    public bool PoseeElPermiso(Guid idUsuario, Permiso permiso)
    {
        return true;
    }
}