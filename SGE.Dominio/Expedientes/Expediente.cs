public class Expediente
{
    private Guid Id { get; set; }
    private CaratulaExpedientes Caratula { get; set; }
    private DateTime FechaCreacion { get; set; }
    private DateTime FechaUltimaModificacion { get; set; }
    private Guid UsuarioUltimoCambio { get; set; }
    private EstadoExpedientes Estado  { get; set; }
    public Expediente(Guid id, CaratulaExpedientes caratula, DateTime fechaCreacion, Guid usuarioUltimoCambio)
    {
        this.Id = Guid.NewGuid();
        this.Caratula = caratula;
        this.FechaCreacion = fechaCreacion;
        this.FechaUltimaModificacion = this.FechaCreacion;
        this.UsuarioUltimoCambio = this.Id;
        this.Estado = EstadoExpedientes.RecienIniciado;
    }
    // Modificar caratula de expediente en caso de error al momento de la creación
    public void ModificarCaratula(CaratulaExpedientes nuevaCaratula, Guid idUsuario)
    {
        this.Caratula = nuevaCaratula;
        this.UsuarioUltimoCambio = idUsuario;
        this.FechaUltimaModificacion = DateTime.Now;
    }
    public void ActualizarEstado (Etiqueta? ultimaEtiqueta, Guid idUsuario)
    {
        if (ultimaEtiqueta == null)
        {
            this.Estado = EstadoExpedientes.RecienIniciado;
        }
        else if (ultimaEtiqueta == Etiqueta.Resolucion)
        {
            this.Estado = EstadoExpedientes.ConResolucion;
        }
        else if (ultimaEtiqueta == Etiqueta.PaseAEstudio)
        {
            this.Estado = EstadoExpedientes.ParaResolver;
        }
        else if (ultimaEtiqueta == Etiqueta.PaseAlArchivo)
        {
            this.Estado = EstadoExpedientes.Finalizado;
        }
        this.UsuarioUltimoCambio = idUsuario;
        this.FechaUltimaModificacion = DateTime.Now;
    }
    public void CambiarEstado(EstadoExpedientes nuevoEstado, Guid idUsuario)
    {
        bool encontreUsuario = false;
        using (StreamReader reader = new StreamReader("ruta_del_archivo_de_estados.txt"))
        {
            //preguntar como saber cual es el id
            while ((!reader.EndOfStream) && (!encontreUsuario))
            {
                if(reader.ReadLine()==idUsuario.ToString())
                {
                    //deberia modificar los archivos
                    encontreUsuario=true;
                }
            }
        }
    }
}