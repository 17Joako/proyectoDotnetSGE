public class ActualizacionEstadoExpedienteService(IExpedienteRepository repo,ITramiteRepository repo2, Guid IDUsuario)
{
    public void ActualizarEstadoExpediente(Guid ID, Guid idUsuario)
    {
        var expediente = repo.ObtenerPorId(ID);
        var tramites = repo2.ObtenerPorExpedienteId(ID);
        Etiqueta? ultimaEtiqueta= null;
        Tramite? ultimoTramite= tramites.firstOrDefault();
        if (tramites!=null){
        foreach (var tramite in tramites)
        {
           if(tramite.fechaUltimaModificacion > ultimoTramite.FechaUltimaModificacion)
            {
                ultimoTramite = tramite;
                ultimaEtiqueta = tramite.Etiqueta;
            }
            }}
           bool cambio= expediente.ActualizarEstado(ultimaEtiqueta, idUsuario);
        if (cambio)
        {
            repo.ModificarExpediente(expediente);
        }
        }

        
        /*string ultimaLinea=null;
        Etiqueta? ultimaEtiqueta;
        using (StreamReader reader = new StreamReader("ruta_del_archivo_de_estados.txt"))
        {
            string linea;
            while ((linea=reader.ReadLine()) != null)
            {
                ultimaLinea = linea;
            }
        }
        if (ultimaLinea == null)
        {
            ultimaEtiqueta = null;
        }
        else
        {
            string[] partes = ultimaLinea.Split(',');
            ultimaEtiqueta = Enum.Parse<Etiqueta>(partes[3]);
        }
        bool cambio= expediente.ActualizarEstado(ultimaEtiqueta, idUsuario);
        if (cambio)
        {
            repo.ModificarExpediente(expediente);
        }*/
    }
}