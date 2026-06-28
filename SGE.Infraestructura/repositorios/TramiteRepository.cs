namespace SGE.Infraestructura;

public class TramiteRepository : ITramiteRepository
{
    private readonly SgeContext _context;

    public TramiteRepository(SgeContext context)
    {
        _context = context;
    }

    public void AgregarTramite(Tramite tramite)
    {
        _context.Tramites.Add(tramite);
    }

    public void EliminarTramite(Guid id)
    {
        var tramite = _context.Tramites.Where(t => t.Id == id).FirstOrDefault();
        if (tramite != null)
        {
            _context.Tramites.Remove(tramite);
        }
        else
        {
            throw new entidadNoEncontradaException("el tramite no existe");
        }
    }

    public void ModificarTramite(Guid Id, ContenidoTramite nuevoContenido, EtiquetaTramites nuevaEtiqueta, Guid nuevoExpedienteId, Guid IdUsusario)
    {
        var tramite = _context.Tramites.Where(t => t.Id == Id).FirstOrDefault();
        if (tramite != null)
        {
            tramite.ModificarTramite(nuevoContenido, nuevaEtiqueta, nuevoExpedienteId, IdUsusario);
            _context.Tramites.Update(tramite);
        }
        else
        {
            throw new entidadNoEncontradaException("el tramite no existe");
        }
    }

    public void EliminarTramitesPorExpedienteId(Guid idExpediente)
    {
        var tramites = _context.Tramites.Where(t => t.ExpedienteId == idExpediente).ToList();
        if (tramites.Any())
        {
            _context.Tramites.RemoveRange(tramites);
        }
        // No hay trámites asociados; no es un error al eliminar el expediente.
    }

    public Tramite ObtenerPorId(Guid id)
    {
        var tramite = _context.Tramites.Where(t => t.Id == id).FirstOrDefault();
        if (tramite != null)
        {
            return tramite;
        }
        else
        {
            throw new entidadNoEncontradaException("el tramite no existe");
        }
    }

    public IEnumerable<Tramite> ObtenerPorExpedienteId(Guid idExpediente)
    {
        var tramites = _context.Tramites.Where(t => t.ExpedienteId == idExpediente).ToList();
        if (tramites.Any())
        {
            return tramites;
        }
        else
        {
            return null;
        }
    }
    public IEnumerable<Tramite> BuscarTodos()
    {
        return _context.Tramites.ToList();
    }
}