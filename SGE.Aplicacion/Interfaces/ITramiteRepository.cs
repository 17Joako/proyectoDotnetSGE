public interface ITramiteRepository
{
    void AgregarTramite(Tramite tramite);
    void EliminarTramite(Guid id);
    void ModificarTramite(ContenidoTramite nuevoContenido, EtiquetaTramites nuevaEtiqueta, Guid nuevoExpedienteId);

    void EliminarTramitesPorExpedienteId(Guid idExpediente);
    Tramite ObtenerPorId(Guid id);
    IEnumerable<Tramite> ObtenerPorExpedienteId(Guid idExpediente);

    IEnumerable<Tramite> BuscarTodos();
}