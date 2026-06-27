/*public class RegistrarUsuarioUseCase(IUsuarioRepository _usuarioRepository, IUnidadDeTrabajo unidadDeTrabajo)
{
    public void Ejecutar(string nombre, string correoElectronico, string contrasena, bool esAdministrador, Permiso permisos)
    {
        // Validar que el correo electrónico no esté registrado
        if (_usuarioRepository.ExisteCorreoElectronico(correoElectronico))
        {
            throw new Exception("El correo electrónico ya está registrado.");
        }

        // Crear un nuevo usuario
        var nuevoUsuario = new Usuario
        {
            Id = Guid.NewGuid(),
            Nombre = nombre,
            CorreoElectronico = correoElectronico,
            ContrasenaHash = HashContrasena(contrasena),
            EsAdministrador = esAdministrador,
            ListaPermisos = permisos
        };

        // Guardar el nuevo usuario en el repositorio
        _usuarioRepository.Agregar(nuevoUsuario);
        unidadDeTrabajo.Guardar();
    }

    private string HashContrasena(string contrasena)
    {
        // Implementar un método de hashing seguro para la contraseña
        // Por ejemplo, utilizando BCrypt o PBKDF2
        return BCrypt.Net.BCrypt.HashPassword(contrasena);
    }
}*/