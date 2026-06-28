//se podria mejorar si hay tiempo hacerlo
public class ModificarMisDatosUseCase(IUsuarioRepository _usuarioRepository, IUnidadDeTrabajo unidadDeTrabajo, IPasswordHasher passwordHasher)
{
    public void Ejecutar(ModificarUsuarioRequest request)
    {
        // Validar que el correo electrónico no esté registrado
        var usuario = _usuarioRepository.ObtenerPorCorreoElectronico(request.CorreoElectronico);
        if (usuario == null)
        {
            throw new entidadNoEncontradaException("El correo electrónico no está registrado.");
        }
        // Crear un nuevo usuario 
        // Guardar el nuevo usuario en el repositorio
        _usuarioRepository.ModificarUsuario(request.Nombre, request.CorreoElectronico, passwordHasher.Hash(request.Contrasena));
        //aca responder que se agrego con exito?preguntar viernes
        unidadDeTrabajo.Guardar();
    }
}