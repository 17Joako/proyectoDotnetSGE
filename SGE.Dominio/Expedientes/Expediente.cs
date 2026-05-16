public class Expediente
// Si querés tmb ponete a ver que onda los errores que saltan acá y en caratulaE
{
    private Guid Id { get; set; }
    private CaratulaExpedientes Caratula { get; set; }
    private DateTime FechaCreacion { get; set; }
    private DateTime FechaUltimaModificacion { get; set; }
    private Guid UsuarioUltimoCambio { get; set; }
    
    private EstadoExpedientes Estado  { get; set; }
    // Hay que implementar lo que nos explicó iann hoy sobre como se recorren los tramites y demás cosas, si tenés duda preguntame que te explico mas facil en llamada

    private Expediente(Guid id, CaratulaExpedientes caratula, DateTime fechaCreacion, DateTime fechaUltimaModificacion, Guid usuarioUltimoCambio, EstadoExpedientes estado)
    {
        this.Id = id;
        this.Caratula = caratula;
        this.FechaCreacion = fechaCreacion;
        this.FechaUltimaModificacion = fechaUltimaModificacion;
        this.UsuarioUltimoCambio = usuarioUltimoCambio;
        this.Estado = estado;
    }   
    //constructor privado para la reconstrucción desde la base de datos
    public Expediente(Guid id, CaratulaExpedientes caratula, DateTime fechaCreacion, DateTime fechaUltimaModificacion, Guid usuarioUltimoCambio)
    {
        this.Id = Guid.NewGuid();
        this.Caratula = caratula;
        this.FechaCreacion = fechaCreacion;
        this.FechaUltimaModificacion = fechaUltimaModificacion;
        this.UsuarioUltimoCambio = Guid.NewGuid();
        this.Estado = EstadoExpedientes.RecienIniciado;
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
    }
    // todavía por terminar: cambiar estado del expediente
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
    //el cambiar estado se supone que modifica el estado del expediente de manera directa independientemente de la etiqueta del tramite

    public void CambiarEstado(EstadoExpedientes nuevoEstado, Guid idUsuario)
    {
        this.Estado = nuevoEstado;
        this.FechaUltimaModificacion = DateTime.Now;
        this.UsuarioUltimoCambio = idUsuario;
    }
 //hice el recostructor del expediente
    public static Expediente Reconstruir(Guid id, CaratulaExpedientes caratula, DateTime fechaCreacion, DateTime fechaUltimaModificacion, Guid usuarioUltimoCambio, EstadoExpedientes estado)
    {
        return new Expediente(id, caratula, fechaCreacion, fechaUltimaModificacion, usuarioUltimoCambio, estado);
    }
}