public class Usuario
{   public Guid Id { get;private set;} //Id del usuario
    public String Nombre { get; private set;}//Nombre del usuario
    public String CorreoElectronico { get; private set;}//Correo electrónico del usuario
    public String ContrasenaHash { get; init; }//Hash de la contraseña del usuario
    public bool EsAdministrador { get;private set;}//Indica si el usuario tiene privilegios de administrador
    public Permiso ListaPermisos { get;private set; }//cuando se modifico la ultima vez la entidad
    
    private Usuario() { } // Constructor privado para EF Core


}