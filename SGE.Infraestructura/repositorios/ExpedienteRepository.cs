namespace SGE.Infraestructura;

public class ExpedienteRepository : IExpedienteRepository
{
    private readonly SgeContext _context;
    public ExpedienteRepository (SgeContext context)
    {
        _context = context;
    }

    // Método para el alta de un expediente
    public void AgregarExpediente(Expediente expediente)
    {
        _context.Expedientes.Add(expediente);
    }

    // Método para la baja de un expediente
    public void EliminarExpediente(Guid idExpediente)
    {
        var expediente = _context.Expedientes.Where(e => e.Id == idExpediente).FirstOrDefault();
        if (expediente != null)
        {
            _context.Expedientes.Remove(expediente);
        }
        else
        {
            throw new entidadNoEncontradaException("El expediente no existe");
        }
    }

    // Método para listar todos los expedientes
    // Devuelve una lista con todos los expedientes en la base de datos
    public IEnumerable<Expediente> BuscarTodos()
    {
        return _context.Expedientes.ToList();
    }

    // Método para cambiar el estado de un expediente manualmente
    public void CambiarEstado(Guid idExpediente, EstadoExpedientes nuevoEstado, Guid idUsuario)
    {
        var expediente = _context.Expedientes.Where(e => e.Id == idExpediente).FirstOrDefault();
        if (expediente != null)
        {
            expediente.CambiarEstado(nuevoEstado, idUsuario);
            _context.Expedientes.Update(expediente);
        }
        else
        {
            throw new entidadNoEncontradaException("El expediente no existe");
        }
    }

    // Método para modificar la caratula de un expediente
    public void ModificarCaratula(Guid idExpediente, CaratulaExpedientes nuevaCaratula, Guid idUsuario)
    {
        var expediente = _context.Expedientes.Where(e => e.Id == idExpediente).FirstOrDefault();
        if (expediente != null)
        {
            expediente.ModificarCaratula(nuevaCaratula, idUsuario);
            _context.Expedientes.Update(expediente);
        }
        else
        {
            throw new entidadNoEncontradaException("El expediente no existe");
        }
    }

    // Método para obtener un expediente por ID
    public Expediente ObtenerExpedientePorId(Guid idExpediente)
    {
        var expediente = _context.Expedientes.Where(e => e.Id == idExpediente).FirstOrDefault();
        if (expediente != null)
        {
            return expediente;
        }
        else
        {
            throw new entidadNoEncontradaException("El expediente no existe");
        }
    } 
}