public class ActualizacionEstadoExpedienteService(IExpedienteRepository repo,ITramiteRepository repo2)
{
    public void ActualizarEstadoExpediente(Guid ID, Guid IDUsuario)
    {
        var expedientes = repo.ObtenerExpedientePorId(ID);
        var tramites = repo2.ObtenerPorExpedienteId(ID);
        EtiquetaTramites? ultimaEtiqueta = null;
        if (tramites!= null){
        Tramite? ultimoTramite = tramites.OrderByDescending(t => t.FechaCreacion).FirstOrDefault();
        ultimaEtiqueta = ultimoTramite?.Etiqueta;
        }
           bool cambio= expedientes.ActualizarEstado(ultimaEtiqueta, IDUsuario);
        if (cambio)
        {
            repo.ModificarCaratula(ID, expedientes.Caratula, IDUsuario);
        }
    }
}