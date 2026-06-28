public class LoginUseCase(IUsuarioRepository _usuarioRepository, IPasswordHasher passwordHasher)
{
    public LoginResponse Ejecutar(LoginRequest request)
    {
        // Validar que el correo electrónico esté registrado
        var usuario = _usuarioRepository.ObtenerPorCorreoElectronico(request.CorreoElectronico);
        if (usuario == null)
        {
            throw new entidadNoEncontradaException("El correo electrónico no está registrado.");
        }
        if (request.Contrasena == null)
        {
            throw new NegocioException("La contraseña no puede ser nula");
        }
        // Verificar la contraseña
        if (!passwordHasher.Verify(request.Contrasena, usuario.ContrasenaHash))
        {
            throw new NegocioException("Contraseña incorrecta.");
        }
        
        var token = Jwt.GenerarJwt(usuario);
        return new LoginResponse(token);
     
    }
}