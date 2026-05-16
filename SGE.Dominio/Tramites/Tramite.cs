public class Tramite
{   public Guid Id { get;}//guid1
    public Guid ExpedienteId { get;}//guid2
    public etiqueta Etiqueta { get;}//esto es enumerativo
    public ContenidoTramite Contenido { get; init; }//aca se almacenan los datos de texto o string
    public DateTime FechaCreacion { get;}//cuando se creo
    public DateTime FechaUltimaModificacion{ get; set; }//cuando se modifico la ultima vez la entidad
    public Guid UsuarioUltimoCambio {get; set; }

    public Tramite(Guid expedienteID,ContenidoTramite contenido)
    {
        Id = Guid.NewGuid();
        ExpedienteId=expedienteID ;
        Etiqueta = 0;
        Contenido = contenido;
        FechaCreacion = DateTime.Now;
        FechaUltimaModificacion = this.FechaCreacion;
        UsuarioUltimoCambio = this.Id;
    }
}