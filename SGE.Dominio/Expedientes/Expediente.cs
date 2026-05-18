public class Expediente
{
    public Guid Id { get;private set; }
    public CaratulaExpedientes Caratula { get; private set; }
    public DateTime FechaCreacion { get;private set; }
    public DateTime FechaUltimaModificacion { get;private set; }
    public Guid UsuarioUltimoCambio { get;private set; }
    public EstadoExpedientes Estado  { get;private set; }

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
    public bool ActualizarEstado (Etiqueta? ultimaEtiqueta, Guid idUsuario)
    {
        if (ultimaEtiqueta == null)
        {
            this.Estado = EstadoExpedientes.RecienIniciado;
            return true;
        }
        else if (ultimaEtiqueta == Etiqueta.Resolucion)
        {
            this.Estado = EstadoExpedientes.ConResolucion;
            return true;
        }
        else if (ultimaEtiqueta == Etiqueta.PaseAEstudio)
        {
            this.Estado = EstadoExpedientes.ParaResolver;
            return true;
        }
        else if (ultimaEtiqueta == Etiqueta.PaseAlArchivo)
        {
            this.Estado = EstadoExpedientes.Finalizado;
            return true;
        }
        this.UsuarioUltimoCambio = idUsuario;//Desconozco cuando llamar esto,
        this.FechaUltimaModificacion = DateTime.Now;
        return false;
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