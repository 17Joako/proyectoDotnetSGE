using System.Collections;

public class TramiteTxtRepository : ITramiteRepository
{ 
    readonly string rutaArchivo = @"..\SGE.Repositorios\Tramite.txt";
    
    // primero agrego el id, y luego agrego el resto de datos
    public void AgregarTramite(Tramite tramite)
    {
        using (StreamWriter sw = new StreamWriter(rutaArchivo, true))
        {
            sw.WriteLine($"{tramite.Id};{tramite.ExpedienteId};{tramite.Etiqueta};{tramite.Contenido.contenido};{tramite.FechaCreacion};{tramite.FechaUltimaModificacion};{tramite.UsuarioUltimoCambio}");
         }
    }

    public void EliminarTramite(Guid id)
    {
        //transformo el txt en una lista de tramites, luego elimino el tramite que quiero eliminar y luego vuelvo a guardar la lista de tramites en el txt
        
        IEnumerable<Tramite> tramites =BuscarTodos();//transformo el txt en una lista de tramites, luego elimino el tramite que quiero eliminar y luego vuelvo a guardar la lista de tramites en el txt
        
        List<Tramite> tramitesList = tramites.ToList();
        
        int cont=tramitesList.Count;
        
        tramitesList.RemoveAll(t => t.Id == id);
        
        GuardarTodos(tramitesList);
        
        if(cont == tramitesList.Count)
        {
            throw new RepositoryException("Trámite no encontrado para eliminar.");
        }
    }

    public Tramite ObtenerPorId(Guid id)
    {
        Tramite? tramiteEncontrado = null;
        var tramites = BuscarTodos();
        foreach (Tramite tramite in tramites)
        {
            if (tramite.Id == id)
            {
                if (tramiteEncontrado == null || tramite.FechaUltimaModificacion > tramiteEncontrado.FechaUltimaModificacion)
                {
                    tramiteEncontrado = tramite;
                }
            }
        }

        return tramiteEncontrado;//si devuelve null, da error en caso de uso
        //esto deberia ser una repositoryException
    }
    
    //devuelve un IEnumerable de tramites, esto se hace para poder mostrar todos los tramites disponibles
    public IEnumerable<Tramite> BuscarTodos()
    {
        List<Tramite> tramites = new List<Tramite>();
        if (File.Exists(rutaArchivo))
        {
            var lineas = File.ReadAllLines(rutaArchivo);
            foreach (var linea in lineas)
            {
                var partes = linea.Split(';');
                if (partes.Length == 7)
                {   
                    tramites.Add(Tramite.Reconstruir
                    (
                        Guid.Parse(partes[0]),
                        Guid.Parse(partes[1]),
                        Enum.Parse<EtiquetaTramites>(partes[2]),
                        new ContenidoTramite(partes[3]),
                        DateTime.Parse(partes[4]),
                        DateTime.Parse(partes[5]),
                        Guid.Parse(partes[6])
                    ));};
                }
            }
        return tramites;
    }
    //rescribe todos los tramites del txt, se hace para poder eliminar/modificar un tramite 
    private void GuardarTodos(List<Tramite> tramites)
    {
        using (StreamWriter sw = new StreamWriter(rutaArchivo, false))
        {
            foreach (var tramite in tramites)
            {
                sw.WriteLine($"{tramite.Id};{tramite.ExpedienteId};{tramite.Etiqueta};{tramite.Contenido.contenido};{tramite.FechaCreacion};{tramite.FechaUltimaModificacion};{tramite.UsuarioUltimoCambio}");
            }
        }
    }
    //este metodo busca el tramite por su id, luego se modifica el tramite y luego se guarda el tramite modificado en el txt
    public void ModificarTramite(Tramite tramite)
    {
        IEnumerable<Tramite> tramites = BuscarTodos();
        List<Tramite> tramitesList = tramites.ToList();
        int index = tramitesList.FindIndex(t => t.Id == tramite.Id);
        if (index != -1)
        {
            tramitesList[index] = tramite;
            GuardarTodos(tramitesList);
        }
        else{throw new RepositoryException("Trámite no encontrado para modificar.");}
    }
    //este metodo es para eliminar todos los tramites que tengan el mismo expedienteId, esto se hace porque si se elimina un expediente, se deben eliminar todos los tramites asociados a ese expediente
    public void EliminarTramitesPorExpedienteId(Guid idExpediente)
    {
        IEnumerable<Tramite> tramites = BuscarTodos();
        List<Tramite> tramitesList = tramites.ToList();
        int cont=tramitesList.Count;
        tramitesList.RemoveAll(t => t.ExpedienteId == idExpediente);
        GuardarTodos(tramitesList);
        if(cont == tramitesList.Count)
        {
            throw new RepositoryException("No se encontraron trámites para eliminar con el expedienteId proporcionado.");
        }
    }

    //este metodo es para obtener todos los tramites que tengan el mismo expedienteId, esto se hace porque si se quiere obtener un expediente
    public IEnumerable<Tramite> ObtenerPorExpedienteId(Guid idExpediente)
    {
        List<Tramite> tramitesEncontrados = new List<Tramite>();
        var tramites = BuscarTodos();
        foreach (Tramite tramite in tramites)
        {
            if (tramite.ExpedienteId == idExpediente)
            {
                tramitesEncontrados.Add(tramite);
            }
        }
        if(tramitesEncontrados.Count > 0)
        {
        return tramitesEncontrados;
        }
        throw new RepositoryException("Trámites no encontrados para el expediente.");
    }
}