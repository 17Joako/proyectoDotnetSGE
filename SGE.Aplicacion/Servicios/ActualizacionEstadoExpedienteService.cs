public class ActualizacionEstadoExpedienteService(IExpedienteRepository repo,ITramiteRepository repo2)
{
    public void ActualizarEstadoExpediente(Guid ID, Guid IDUsuario)
    {
        var expedientes = repo.ObtenerExpedientePorId(ID);
        var tramites = repo2.ObtenerPorExpedienteId(ID);
        EtiquetaTramites? ultimaEtiqueta = null;
        Tramite? ultimoTramite = tramites.FirstOrDefault();
        foreach (var tramite in tramites)
        {
            if(tramite.FechaCreacion > ultimoTramite.FechaCreacion)
                {
                    ultimoTramite = tramite;
                    ultimaEtiqueta = tramite.Etiqueta;
                }
        }
           bool cambio= expedientes.ActualizarEstado(ultimaEtiqueta, IDUsuario);
        if (cambio)
        {
            repo.ModificarCaratula(ID, expedientes.Caratula, IDUsuario);
        }
    }
}