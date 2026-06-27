public class Tramite
{
    public Guid Id { get; private set; } //guid del Tramite
    public Guid ExpedienteId { get; private set; } //recibe guid del Expediente 
    public EtiquetaTramites Etiqueta { get; private set; } //esto es el tipo enumerativo
    public ContenidoTramite? Contenido { get; private set; } //aca se almacenan los datos de texto o string
    public DateTime FechaCreacion { get; private set; } //cuando se creo
    public DateTime FechaUltimaModificacion { get; private set; } //cuando se modifico la ultima vez la entidad
    public Guid UsuarioUltimoCambio { get; private set; } //quien fue la ultima persona que lo modifico

    protected Tramite()
    {
    }

    // constructor para altas de tramites
    public Tramite(Guid expedienteId, ContenidoTramite contenido)
    {
        var fechaCreacion = DateTime.Now;
        Id = Guid.NewGuid();
        ExpedienteId = expedienteId;
        Etiqueta = default;
        Contenido = contenido;
        FechaCreacion = fechaCreacion;
        FechaUltimaModificacion = fechaCreacion;
        UsuarioUltimoCambio = expedienteId;
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

    //metodo para modificar el tramite, se modifica el contenido, la etiqueta, el expediente id, la fecha de ultima modificacion y el usuario que hizo el ultimo cambio
    public void ModificarTramite(ContenidoTramite nuevoContenido, EtiquetaTramites nuevaEtiqueta, Guid nuevoExpedienteId, Guid usuarioId)
    {
        this.Contenido = nuevoContenido;
        this.Etiqueta = nuevaEtiqueta;
        this.ExpedienteId = nuevoExpedienteId;
        FechaUltimaModificacion = DateTime.Now;
        UsuarioUltimoCambio = usuarioId;
    }

}