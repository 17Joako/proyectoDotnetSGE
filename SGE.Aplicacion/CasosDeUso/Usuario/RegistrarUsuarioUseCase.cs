using System.Security.Cryptography;
public class RegistrarUsuarioUseCase
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUnidadDeTrabajo _unidadDeTrabajo;

    // Inyectamos todas las dependencias mediante el constructor
    public RegistrarUsuarioUseCase(
        IUsuarioRepository usuarioRepository,
        IPasswordHasher passwordHasher,
        IUnidadDeTrabajo unidadDeTrabajo)
    {
        _usuarioRepository = usuarioRepository;
        _passwordHasher = passwordHasher;
        _unidadDeTrabajo = unidadDeTrabajo;
    }

    public void Ejecutar(RegistrarUsuarioRequest request)
    {
        // 1. Validar que el correo electrónico no esté registrado
        var usuarioExistente = _usuarioRepository.ObtenerPorCorreoElectronico(request.CorreoElectronico);
        if (usuarioExistente != null)
        {
            // NOTA: Asegurate de que NegocioException herede de DominioException 
            // para que tu Manejador Global lo transforme en un 400 Bad Request.
            throw new DominioException("El correo electrónico ya está registrado.");
        }

        // 2. Encriptar la contraseña y guardar en el repositorio utilizando los campos privados
        _usuarioRepository.Agregar(
            request.Nombre,
            request.CorreoElectronico,
            _passwordHasher.Hash(request.Contrasena),
            request.esAdministrador,
            request.permisosUsuario
        );

        // 3. Confirmar los cambios en la Base de Datos
        _unidadDeTrabajo.Guardar();
    }
}