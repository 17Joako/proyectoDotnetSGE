public class RegistrarUsuarioUseCase(IUsuarioRepository _usuarioRepository)
{
    public void Ejecutar(RegistrarUsuarioRequest request)
    {
        // Validar que el correo electrónico no esté registrado
        var usuario = _usuarioRepository.BuscarPersona(request.CorreoElectronico);
        if (usuario != null)
        {
            throw new NegocioException("El correo electrónico ya está registrado.");
        }
        // Crear un nuevo usuario 
        // Guardar el nuevo usuario en el repositorio
        _usuarioRepository.Agregar(
            request.Nombre,
            request.CorreoElectronico,
            request.Contrasena//esto lo modifica en el repository o aca
            );//aca responder que se agrego con exito?preguntar viernes
    }
}