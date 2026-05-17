public class AgregarExpedienteUseCase
{
    public void Ejecutar(Guid idUsuario, CaratulaExpedientes caratula, IRepositorioExpedientes repositorio, IAutorizacionService autorizacionService)
    {
        if (!autorizacionService.PoseeElPermiso(idUsuario))
        {
            throw new UnauthorizedAccessException("El usuario no tiene permiso para agregar un expediente.");
        }
        var nuevoExpediente = new Expediente(Guid.NewGuid(), caratula, DateTime.Now, idUsuario);
        repositorio.Agregar(nuevoExpediente);
    }
}