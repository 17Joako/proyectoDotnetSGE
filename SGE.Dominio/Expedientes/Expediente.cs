using Expedientes;
public class Expediente
{
    private Guid Id { get; set; }
    private CaratulaExpedientes Caratula { get; set; }
    private DateTime FechaCreacion { get; set; }
    private DateTime FechaUltimaModificacion { get; set; }
    private Guid UsuarioUltimoCambio { get; set; }
    
    private EstadoExpediente Estado  { get; set; }

    public Expediente(Guid id, CaratulaExpedientes caratula, DateTime fechaCreacion, DateTime fechaUltimaModificacion, Guid usuarioUltimoCambio)
    {
        this.Id = new Guid();
        this.Caratula = new CaratulaExpedientes(caratula);
        this.FechaCreacion = fechaCreacion;
        this.FechaUltimaModificacion = fechaUltimaModificacion;
        this.UsuarioUltimoCambio = new Guid();
        this.Estado = EstadoExpediente.RecienIniciado;
    }

    public bool ActualizarEstado (EtiquetaTramite? ultimaEtiqueta, Guid idUsuario)
    {
        if (ultimaEtiqueta == null)
        {
            this.setEstado(EstadoExpediente.RecienIniciado);
        }
        else if (ultimaEtiqueta == EtiquetaTramite.Resolucion)
        {
            this.setEstado(EstadoExpediente.ConResolucion);
        }
        else if (ultimaEtiqueta == EtiquetaTramite.PaseAEstudio)
        {
            this.setEstado(EstadoExpediente.ParaResolver);
        }
        else if (ultimaEtiqueta == EstadoExpediente.PaseAlArchivo)
        {
            this.setEstado(EstadoExpediente.Finalizado);
        }
        else
        {
            return false;
        }
    }
}