using System.Collections;

public class TramiteTxtRepository : ITramiteRepository
{   //debo hacer 4 cosas principalmente escribir el txt, modificar el txt, eliminar el txt y buscar en el txt,  luego debo hacer los metodos correspondientes para cada una de esas acciones 
    readonly string rutaArchivo = @"..\SGE.Repositorios\Tramite.txt";
    
    /*
    Se podria hacer asi para que el repositorio sea reutilizable y no dependa de una ruta fija, 
    sino que se le pueda pasar la ruta al crear una instancia del repositorio
    Por facilidad, daremos nosotros la ruta
    public TramiteTxtRepository(string rutaArchivo)
    {
        this.rutaArchivo = rutaArchivo;
    }*/
    // primero agrego el id, y luego agrego el resto de datos
    public void AgregarTramite(Tramite tramite)
    {
        using (StreamWriter sw = new StreamWriter(rutaArchivo, true))
        {
            sw.WriteLine($"{tramite.Id};{tramite.ExpedienteId};{tramite.Etiqueta};{tramite.Contenido.contenido};{tramite.FechaCreacion};{tramite.FechaUltimaModificacion};{tramite.UsuarioUltimoCambio}");
         }
    }

    public void EliminarTramite(Guid id)
    {//Tengo que chequear despues si esto es correcto, o deberia hacer uno diferente si la persona desea eliminar solo 1 Tramite
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
    {//esto deberia retornar el id con fecha mas reciente
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
        if(tramiteEncontrado != null)
        {
            return tramiteEncontrado;
        }
        throw new RepositoryException("Trámite no encontrado.");//esto deberia ser una repositoryException
    }
    

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
                        Enum.Parse<Etiqueta>(partes[2]),
                        new ContenidoTramite(partes[3]),
                        DateTime.Parse(partes[4]),
                        DateTime.Parse(partes[5]),
                        Guid.Parse(partes[6])
                    ));};
                }
            }
        return tramites;
    }
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
        throw new RepositoryException("Trámite no encontrado para modificar.");
    }
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