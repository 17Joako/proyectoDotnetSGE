public class Expediente
{
    public Guid Id { get;private set; }
    public CaratulaExpedientes Caratula { get; private set; }
    public DateTime FechaCreacion { get;private set; }
    public DateTime FechaUltimaModificacion { get;private set; }
    public Guid UsuarioUltimoCambio { get;private set; }
    public EstadoExpedientes Estado  { get;private set; }
    
    public Expediente(CaratulaExpedientes caratula, DateTime fechaCreacion)
    {
        new Expediente(Guid.NewGuid(), caratula, fechaCreacion, fechaCreacion, this.Id, EstadoExpedientes.RecienIniciado);
    }
    // Modificar caratula de expediente en caso de error al momento de la creación
    public void ModificarCaratula(CaratulaExpedientes nuevaCaratula, Guid idUsuario,DateTime fechaCambio)
    {
        this.Caratula = nuevaCaratula;
        this.UsuarioUltimoCambio = idUsuario;
        this.FechaUltimaModificacion = fechaCambio;
    }
    public bool ActualizarEstado (EtiquetaTramites? ultimaEtiqueta, Guid idUsuario)
    {
        if (ultimaEtiqueta == null)
        {
            this.Estado = EstadoExpedientes.RecienIniciado;
            return true;
        }
        else if (ultimaEtiqueta == EtiquetaTramites.Resolucion)
        {
            this.Estado = EstadoExpedientes.ConResolucion;
            return true;
        }
        else if (ultimaEtiqueta == EtiquetaTramites.PaseAEstudio)
        {
            this.Estado = EstadoExpedientes.ParaResolver;
            return true;
        }
        else if (ultimaEtiqueta == EtiquetaTramites.PaseAlArchivo)
        {
            this.Estado = EstadoExpedientes.Finalizado;
            return true;
        }
        this.UsuarioUltimoCambio = idUsuario;
        this.FechaUltimaModificacion = DateTime.Now;
        return false;
    }
    public void CambiarEstado(EstadoExpedientes nuevoEstado, Guid idUsuario)
    {
        this.Estado = nuevoEstado;
        this.FechaUltimaModificacion = DateTime.Now;
        this.UsuarioUltimoCambio = idUsuario;
    }
    //constructor privado para la reconstrucción desde la base de datos
    private Expediente(Guid id, CaratulaExpedientes caratula, DateTime fechaCreacion, DateTime fechaUltimaModificacion, Guid usuarioUltimoCambio, EstadoExpedientes estado)
    {
        this.Id = id;
        this.Caratula = caratula;
        this.FechaCreacion = fechaCreacion;
        this.FechaUltimaModificacion = fechaUltimaModificacion;
        this.UsuarioUltimoCambio = usuarioUltimoCambio;
        this.Estado = estado;
    }  
    public static Expediente Reconstruir(Guid id, CaratulaExpedientes caratula, DateTime fechaCreacion, DateTime fechaUltimaModificacion, Guid usuarioUltimoCambio, EstadoExpedientes estado)
    {
        return new Expediente(id, caratula, fechaCreacion, fechaUltimaModificacion, usuarioUltimoCambio, estado);
    }
}