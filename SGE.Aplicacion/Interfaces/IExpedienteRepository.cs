public interface IExpedienteRepository
{
    void AgregarExpediente(Expediente expediente);
    void EliminarExpediente(Guid id);
    void ModificarExpediente(Expediente expediente);
    // Charlar con finn en un rato
    //void ModificarCaratula(Guid id, CaratulaExpedientes caratula);

    //FINNISIMO DESPUES EXPLICA MAINTOOOOOOOO
    //void CambiarEstado(Guid id, EstadoExpedientes estado);
    Expediente ObtenerPorId(Guid id);
    IEnumerable<Expediente> BuscarTodos();
}