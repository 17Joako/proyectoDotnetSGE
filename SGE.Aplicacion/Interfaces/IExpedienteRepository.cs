public interface IExpedienteRepository
{
    void AgregarExpediente(Expediente expediente);
    void EliminarExpediente(Guid id);
    void ModificarExpediente(Expediente expediente);
    void ModificarCaratula(Guid id, CaratulaExpedientes caratula);

    void CambiarEstado(Guid id, EstadoExpedientes estado);
    Expediente ObtenerPorId(Guid id);
    IEnumerable<Expediente> BuscarTodos();
}