public interface IExpedienteRepository
{
    void AgregarExpediente(Expediente expediente);
    void EliminarExpediente(Guid idExpediente);
    void CambiarEstado(Guid idExpediente, EstadoExpedientes nuevoEstado, Guid idUsuario);
    void ModificarCaratula(Guid idExpediente, CaratulaExpedientes nuevaCaratula, Guid idUsuario);
    IEnumerable<Expediente> BuscarTodos();
    Expediente ObtenerExpedientePorId(Guid idExpediente);
}