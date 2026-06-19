public class Tramite
{   public Guid Id { get;private set;} //guid del Tramite
    public Guid ExpedienteId { get; private set;}//recibe guid del Expediente 
    public EtiquetaTramites Etiqueta { get; private set;}//esto es el tipo enumerativo
    public ContenidoTramite Contenido { get; init; }//aca se almacenan los datos de texto o string
    public DateTime FechaCreacion { get;private set;}//cuando se creo
    public DateTime FechaUltimaModificacion{ get;private set; }//cuando se modifico la ultima vez la entidad
    public Guid UsuarioUltimoCambio {get;private set; }//quien fue la ultima persona que lo modifico

    // constructor para altas de tramites
    public Tramite(Guid expedienteId,ContenidoTramite contenido) 
    {
        DateTime FechaCreacion = DateTime.Now;
        new Tramite(Guid.NewGuid(), expedienteId, 0, contenido, FechaCreacion, FechaCreacion, expedienteId);
    }
   // Constructor privado para la reconstrucción desde la base de datos
   private Tramite (Guid id, Guid expedienteId, EtiquetaTramites etiqueta,ContenidoTramite contenido,DateTime fechaCreacion,DateTime fechaUltimaModificacion,Guid usuarioUltimoCambio) 
    {
        Id = id;
        ExpedienteId=expedienteId;
        Etiqueta=etiqueta;
        Contenido = contenido;
        FechaCreacion=fechaCreacion;
        FechaUltimaModificacion = fechaUltimaModificacion;
        UsuarioUltimoCambio=usuarioUltimoCambio;
    } 
    public static Tramite Reconstruir(Guid id, Guid expedienteId, EtiquetaTramites etiqueta,ContenidoTramite contenido,DateTime fechaCreacion,DateTime fechaUltimaModificacion,Guid usuarioUltimoCambio)
    {
         //me mandan los datos desde la BD y yo reconstruyo el objeto
        return new Tramite(id, expedienteId, etiqueta, contenido, fechaCreacion, fechaUltimaModificacion, usuarioUltimoCambio);
    }

}