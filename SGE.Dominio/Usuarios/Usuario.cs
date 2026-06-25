public class Usuario
{
    public Guid Id { get; private set;} //guid de usuario
    public string Nombre { get; private set;} //Nombvre del usuario
    public string CorreoElectronico { get; private set;} //Correo electrónico del usuario
    public string ContrasenaHash { get; private set;} //Contraseña del usuario en formato Hash
    public bool EsAdministrador { get; private set;} //Flag para marcar un usuario como admin
    public List<PermisoUsuarios> ListaPermisos { get; private set;} //Lista de todos los permisos que el usuario posee

    public string Salt { get; private set;} //Salt para el hash de la contraseña
    //Constructor para el registro de un nuevo usuario
    public Usuario (string Nombre, string CorreoElectronico,String salt, string ContrasenaHash, bool EsAdministrador, List<PermisoUsuarios> listaPermisos)
    {
        if (string.IsNullOrEmpty(Nombre) || string.IsNullOrEmpty(CorreoElectronico) || string.IsNullOrEmpty(ContrasenaHash))
        {
            throw new ArgumentException("Todos los campos son requeridos.");
        }
        this.Id = Guid.NewGuid();
        this.Nombre = Nombre;
        this.CorreoElectronico = CorreoElectronico;
        this.Salt = Salt;
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
        this.Salt = Salt;
        this.ContrasenaHash = ContrasenaHash;
        this.EsAdministrador = EsAdministrador;
        this.ListaPermisos = listaPermisos;
    }
    //Recontructor de Usuario
    public static Usuario Reconstruir(Guid id, string Nombre, string CorreoElectronico, string ContrasenaHash, bool EsAdministrador, List<PermisoUsuarios> listaPermisos)
    {
        return new Usuario(id, Nombre, CorreoElectronico, ContrasenaHash, EsAdministrador, listaPermisos);
    }
    

    public void ModificarUsuario(string nombre, string correoElectronico, string contrasenaHash)
    {
        if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(correoElectronico) || string.IsNullOrEmpty(contrasenaHash))
        {
            throw new ArgumentException("Todos los campos son requeridos.");
        }
        this.Nombre = nombre;
        this.CorreoElectronico = correoElectronico;
        this.ContrasenaHash = contrasenaHash;
    }
    public void ModificarPermisos(List<PermisoUsuarios> permisos)
    {
        this.ListaPermisos = permisos;
    }
}