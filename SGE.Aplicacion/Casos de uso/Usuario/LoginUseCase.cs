public class LoginUseCase(IUsuarioRepository _usuarioRepository)
{
    public LoginResponse Ejecutar(LoginRequest request)
    {
        // Validar que el correo electrónico esté registrado
            var usuario = _usuarioRepository.BuscarPersona(request.CorreoElectronico);
            if (usuario == null)
        {
            throw new NegocioException("El correo electrónico no está registrado.");
        }

        // Verificar la contraseña
        if (!usuario.ContrasenaHash.Equals(request.Contrasena))//tengo que buscar como hashear la contraseña 
        {
            throw new NegocioException("Contraseña incorrecta.");
        }
        
        return new LoginResponse(usuario);//esto deberia cambiarlo a solo id de la persona
    }
}