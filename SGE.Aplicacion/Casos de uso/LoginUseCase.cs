public class LoginUseCase(IUsuarioRepository _usuarioRepository)
{
    public Usuario Ejecutar(string correoElectronico, string contrasena)
    {
        // Validar que el correo electrónico esté registrado
        var usuario = _usuarioRepository.ObtenerPorCorreoElectronico(correoElectronico);
        if (usuario == null)
        {
            throw new Exception("El correo electrónico no está registrado.");
        }

        // Verificar la contraseña
        //Tengo que ver como se hace esto
        /*if ()Tengo que ver como se hace esto

        {
            throw new Exception("Contraseña incorrecta.");
        }
*/
        return usuario;
    }
}