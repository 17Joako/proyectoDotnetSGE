public class Usuario
{
    public Guid Id { get; private set;} //guid de usuario
    public string Nombre { get; private set;} //Nombvre del usuario
    public string CorreoElectronico { get; private set;} //Correo electrónico del usuario
    public string ContrasenaHash { get; private set;} //Contraseña del usuario en formato Hash
    public bool EsAdministrador { get; private set;} //Flag para marcar un usuario como admin
    public List<PermisoUsuarios> ListaPermisos { get; private set;} //Lista de todos los permisos que el usuario posee

    //Constructor para el registro de un nuevo usuario
    public Usuario (string Nombre, string CorreoElectronico, string ContrasenaHash, bool EsAdministrador, List<PermisoUsuarios> listaPermisos)
    {
        this.Id = Guid.NewGuid();
        this.Nombre = Nombre;
        this.CorreoElectronico = CorreoElectronico;
        this.ContrasenaHash = ContrasenaHash;
        this.EsAdministrador = EsAdministrador;
        this.ListaPermisos = listaPermisos;
    }
    //Constructor privado para la reconstrucción desde la base de datos
    private Usuario(Guid id, string Nombre, string CorreoElectronico, string ContrasenaHash, bool EsAdministrador, List<PermisoUsuarios> listaPermisos)
    {
        this.Id = id;
        this.Nombre = Nombre;
        this.CorreoElectronico = CorreoElectronico;
        this.ContrasenaHash = ContrasenaHash;
        this.EsAdministrador = EsAdministrador;
        this.ListaPermisos = listaPermisos;
    }
    //Recontructor de Usuario
    public static Usuario Reconstruir(Guid id, string Nombre, string CorreoElectronico, string ContrasenaHash, bool EsAdministrador, List<PermisoUsuarios> listaPermisos)
    {
        return new Usuario(id, Nombre, CorreoElectronico, ContrasenaHash, EsAdministrador, listaPermisos);
    }
}