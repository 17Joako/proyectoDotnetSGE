using Microsoft.EntityFrameworkCore;
namespace SGE.Infraestructura;
public class UnidadDeTrabajoRepository : IUnidadDeTrabajo   {
 private readonly SgeContext _context;
    public UnidadDeTrabajoRepository(SgeContext context)
    {
        _context = context;
    }


    public void Guardar()
    {
        _context.SaveChanges();
    }
}