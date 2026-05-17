public class Expediente
{
    public Guid Id { get;private set; }
    public CaratulaExpedientes Caratula { get; private set; }
    public DateTime FechaCreacion { get;private set; }
    public DateTime FechaUltimaModificacion { get;private set; }
    public Guid UsuarioUltimoCambio { get;private set; }
    public EstadoExpedientes Estado  { get;private set; }
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
    public void ModificarCaratula(String nuevaCaratula, Guid idUsuario,DateTime fechaCambio)
    {
        this.Caratula = new CaratulaExpedientes(nuevaCaratula);//esto lo modifico, despues charlar lo de init
        this.UsuarioUltimoCambio = idUsuario;
        this.FechaUltimaModificacion = fechaCambio;//Esto lo cambie..Bauti
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
        this.UsuarioUltimoCambio = idUsuario;//Desconozco cuando llamar esto,
        this.FechaUltimaModificacion = DateTime.Now;
    }
        
    public void CambiarEstado(EstadoExpedientes nuevoEstado, Guid idUsuario, DateTime fechaCambio,ContenidoTramite? contenidoTramite)
    {
        this.Estado = nuevoEstado;
        this.UsuarioUltimoCambio = idUsuario;
        this.FechaUltimaModificacion = fechaCambio;
    }
    /*public void CambiarEstado(EstadoExpedientes nuevoEstado, Guid idUsuario)
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
    }*/
}