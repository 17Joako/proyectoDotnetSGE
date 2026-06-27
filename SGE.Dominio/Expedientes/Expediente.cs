public class Expediente
{
    public Guid Id { get; private set; } // ID del expediente
    public CaratulaExpedientes? Caratula { get; private set; } // Caratula del expediente
    public DateTime FechaCreacion { get; private set; } // Fecha de la creación del expediente
    public DateTime FechaUltimaModificacion { get; private set; } // Fecha de la última modificación del expediente
    public Guid UsuarioUltimoCambio { get; private set; } // ID del usuario que realizó la última modificación
    public EstadoExpedientes Estado { get; private set; } // Estado del expediente (RecienIniciado, ParaResolver, ConResolucion, etc.)

    protected Expediente()
    {
    }

    // Constructor de un nuevo expediente. Recibe la carátula, la fecha de la creación y el ID del usuario que lo está creando
    // Se genera el ID del expediente al momento de la creación y se establece en estado "Recien Iniciado"
    public Expediente(CaratulaExpedientes caratula, DateTime fechaCreacion, Guid idUsuario)
    {
        Id = Guid.NewGuid();
        Caratula = caratula;
        FechaCreacion = fechaCreacion;
        FechaUltimaModificacion = fechaCreacion;
        UsuarioUltimoCambio = idUsuario;
        Estado = EstadoExpedientes.RecienIniciado;
    }
    // Modificar caratula de expediente en caso de error al momento de la creación
    public void ModificarCaratula(CaratulaExpedientes nuevaCaratula, Guid idUsuario)
    {
        this.Caratula = nuevaCaratula;
        this.UsuarioUltimoCambio = idUsuario;
        this.FechaUltimaModificacion = DateTime.Now;
    }
    // Actualiza el estado del expediente automáticamente en base a la etiqueta del último tramite, que se recibe por parámetro, y el ID del usuario que solicita el cambio
    public bool ActualizarEstado (EtiquetaTramites? ultimaEtiqueta, Guid idUsuario)
    {
        this.UsuarioUltimoCambio = idUsuario;
        if (ultimaEtiqueta == null)
        {
            this.Estado = EstadoExpedientes.RecienIniciado;
            return true;
        }
        else if (ultimaEtiqueta == EtiquetaTramites.PaseAEstudio)
        {
            this.Estado = EstadoExpedientes.ParaResolver;
            return true;
        }
        else if (ultimaEtiqueta == EtiquetaTramites.Resolucion)
        {
            this.Estado = EstadoExpedientes.ConResolucion;
            return true;
        }
        else if (ultimaEtiqueta == EtiquetaTramites.PaseAlArchivo)
        {
            this.Estado = EstadoExpedientes.Finalizado;
            return true;
        }
        this.FechaUltimaModificacion = DateTime.Now;
        return false;
    }
    // Cambia manualmente el estado del expediente al estado especificado
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