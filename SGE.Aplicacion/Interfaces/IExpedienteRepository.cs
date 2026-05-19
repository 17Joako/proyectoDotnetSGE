public interface IExpedienteRepository
{
    void AgregarExpediente(Expediente expediente);
    void EliminarExpediente(Guid id);
    void ModificarExpediente(Expediente expediente);
    Expediente ObtenerPorId(Guid id);
    IEnumerable<Expediente> BuscarTodos();
}