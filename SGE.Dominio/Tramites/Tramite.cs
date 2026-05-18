public class Tramite
{   public Guid Id { get;private set;} //guid del Tramite
    public Guid ExpedienteId { get; private set;}//recibe guid del Expediente 
    public Etiqueta Etiqueta { get; private set;}//esto es el tipo enumerativo
    private ContenidoTramite Contenido { get; init; }//aca se almacenan los datos de texto o string
    public DateTime FechaCreacion { get;private set;}//cuando se creo
    public DateTime FechaUltimaModificacion{ get;private set; }//cuando se modifico la ultima vez la entidad
    public Guid UsuarioUltimoCambio {get;private set; }//quien fue la ultima persona que lo modifico

   private Tramite (Guid id, Guid expedienteId, Etiqueta etiqueta,ContenidoTramite contenido,DateTime fechaCreacion,DateTime fechaUltimaModificacion,Guid usuarioUltimoCambio) // Constructor privado para la reconstrucción desde la base de datos
    {
        Id = id;
        ExpedienteId=expedienteId;
        Etiqueta=etiqueta;
        Contenido = contenido;
        FechaCreacion=fechaCreacion;
        FechaUltimaModificacion = fechaUltimaModificacion;
        UsuarioUltimoCambio=usuarioUltimoCambio;
    } 
    public Tramite(Guid expedienteId,ContenidoTramite contenido)
    {
        //creo el tramite
        Id = Guid.NewGuid();
        ExpedienteId=expedienteId ;
        Etiqueta = 0;
        Contenido = contenido;
        FechaCreacion = DateTime.Now;
        FechaUltimaModificacion = this.FechaCreacion;
        UsuarioUltimoCambio = this.ExpedienteId;
    }
    public static Tramite Reconstruir(Guid id, Guid expedienteId, Etiqueta etiqueta,ContenidoTramite contenido,DateTime fechaCreacion,DateTime fechaUltimaModificacion,Guid usuarioUltimoCambio)
    {
         //me mandan los datos desde la BD y yo reconstruyo el objeto
        return new Tramite(id, expedienteId, etiqueta, contenido, fechaCreacion, fechaUltimaModificacion, usuarioUltimoCambio);
    }

}