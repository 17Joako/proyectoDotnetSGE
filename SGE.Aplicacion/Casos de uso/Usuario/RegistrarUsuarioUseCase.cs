using System.Security.Cryptography;
public class RegistrarUsuarioUseCase(IUsuarioRepository _usuarioRepository, IUnidadDeTrabajo unidadDeTrabajo, IPasswordHasher passwordHasher)
{
    public void Ejecutar(RegistrarUsuarioRequest request)
    {
        // Validar que el correo electrónico no esté registrado
        var usuario = _usuarioRepository.ObtenerPorCorreoElectronico(request.CorreoElectronico);
        if (usuario != null)
        {
            throw new NegocioException("El correo electrónico ya está registrado.");
        }
        // Generar un salt
        var salt = passwordHasher.GenerateSalt();
        // Crear un nuevo usuario 
        // Guardar el nuevo usuario en el repositorio

        _usuarioRepository.Agregar(
            request.Nombre,
            request.CorreoElectronico,
            salt,
            passwordHasher.Hash(request.Contrasena, salt)//preguntar el viernes
            );
        unidadDeTrabajo.Guardar();
    }
}