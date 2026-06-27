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
        // Crear un nuevo usuario 
        // Guardar el nuevo usuario en el repositorio

        _usuarioRepository.Agregar(
            request.Nombre,
            request.CorreoElectronico,
            passwordHasher.Hash(request.Contrasena),
            request.esAdministrador,
            request.permisosUsuario
            );
        unidadDeTrabajo.Guardar();
    }
}