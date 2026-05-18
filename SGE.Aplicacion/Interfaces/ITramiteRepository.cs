public interface ITramiteRepository
{
    void AgregarTramite(Tramite tramite);
    void EliminarTramite(Guid id);
    void ModificarTramite(Tramite tramite);

    void EliminarTramitesPorExpedienteId(Guid idExpediente);
    Tramite ObtenerPorId(Guid id);
    IEnumerable<Tramite> ObtenerPorExpedienteid(Guid idExpediente);
}