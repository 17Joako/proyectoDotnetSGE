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
        else
        {
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
}