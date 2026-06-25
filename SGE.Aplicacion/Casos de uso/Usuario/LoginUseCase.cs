public class LoginUseCase(IUsuarioRepository _usuarioRepository, IPasswordHasher passwordHasher)
{
    public LoginResponse Ejecutar(LoginRequest request)
    {
        // Validar que el correo electrónico esté registrado
            var usuario = _usuarioRepository.ObtenerPorCorreoElectronico(request.CorreoElectronico);
            if (usuario == null)
        {
            throw new NegocioException("El correo electrónico no está registrado.");
        }

        // Verificar la contraseña
        if (!passwordHasher.Verify(request.Contrasena, usuario.Salt, usuario.ContrasenaHash))
        {
            throw new NegocioException("Contraseña incorrecta.");
        }
        
        return new LoginResponse(usuario);//esto deberia cambiarlo a solo id de la persona
    }
}