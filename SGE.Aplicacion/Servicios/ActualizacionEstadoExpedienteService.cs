public class ActualizacionEstadoExpedienteService(IExpedienteRepository repo,ITramiteRepository repo2)
{
    public void ActualizarEstadoExpediente(Guid ID, Guid IDUsuario)
    {
        var expediente = repo.ObtenerPorId(ID);
        var tramites = repo2.ObtenerPorExpedienteId(ID);
        Etiqueta? ultimaEtiqueta = null;
        Tramite? ultimoTramite = tramites.FirstOrDefault();
        foreach (var tramite in tramites)
        {
            if(tramite.FechaUltimaModificacion > ultimoTramite.FechaUltimaModificacion)
                {
                    ultimoTramite = tramite;
                    ultimaEtiqueta = tramite.Etiqueta;
                }
        }
           bool cambio= expediente.ActualizarEstado(ultimaEtiqueta, IDUsuario);
        if (cambio)
        {
            repo.ModificarExpediente(expediente);
        }
    }
} // me está tirando un erro que nose que es, pero solo me sale a mi, no a mis dos compañeros