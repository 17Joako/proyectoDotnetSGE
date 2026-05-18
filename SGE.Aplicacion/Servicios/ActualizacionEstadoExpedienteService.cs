public class ActualizacionEstadoExpedienteService(IExpedienteRepository repo, Guid IDUsuario)
{
    public void ActualizarEstadoExpediente(Guid ID, Guid idUsuario)
    {
        var expediente = repo.ObtenerPorId(ID);
        //aca hay que hacer el recorrido de los tramites para obtener el ultimo pero no se como sacar la data del txt, probe con un split porq me lo dijo google, npi si esta bien
        string? ultimaLinea=null;
        Etiqueta? ultimaEtiqueta;
        using (StreamReader reader = new StreamReader("ruta_del_archivo_de_estados.txt"))
        {
            string? linea;
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
        }
    }
}