//se podria mejorar si hay tiempo hacerlo
public class ModificarMisDatosUseCase(IUsuarioRepository _usuarioRepository)
{
    public void Ejecutar(ModificarUsuarioRequest request)
    {
        // Validar que el correo electrónico no esté registrado
        var usuario = _usuarioRepository.BuscarPersona(request.CorreoElectronico);
        if (usuario != null)
        {
            throw new NegocioException("El correo electrónico ya está registrado.");
        }
        // Crear un nuevo usuario 
        // Guardar el nuevo usuario en el repositorio
        _usuarioRepository.ModificarUsuario(request.Nombre, request.CorreoElectronico, request.Contrasena);
        //aca responder que se agrego con exito?preguntar viernes
    }
}