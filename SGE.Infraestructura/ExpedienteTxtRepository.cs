public class ExpedienteTxtRepository
{   //debo hacer 4 cosas principalmente escribir el txt, modificar el txt, eliminar el txt y buscar en el txt,  luego debo hacer los metodos correspondientes para cada una de esas acciones 
    readonly string rutaArchivo = @"..\SGE.Repositorios\Expediente.txt";
    
    /*
    Se podria hacer asi para que el repositorio sea reutilizable y no dependa de una ruta fija, 
    sino que se le pueda pasar la ruta al crear una instancia del repositorio
    Por facilidad, daremos nosotros la ruta
    public TramiteTxtRepository(string rutaArchivo)
    {
        this.rutaArchivo = rutaArchivo;
    }*/
    // primero agrego el id, y luego agrego el resto de datos
    public void AgregarExpediente(Expediente expediente)
    {
        using (StreamWriter sw = new StreamWriter(rutaArchivo, true))
        {
            sw.WriteLine($"{expediente.Id};{expediente.Caratula};{expediente.FechaCreacion};{expediente.FechaUltimaModificacion};{expediente.UsuarioUltimoCambio};{expediente.Estado}");
         }
    }

    public void EliminarExpediente(Guid id)
    {//Tengo que chequear despues si esto es correcto, o deberia hacer uno diferente si la persona desea eliminar solo 1 expediente

        IEnumerable<Expediente> datosProtegidos = BuscarTodos();//recibo un IEnumerable
        
        List<Expediente> expedientes = datosProtegidos.ToList();//convierto el numerable en lista 
        
        expedientes.RemoveAll(t => t.Id == id);//deberia ver si es removeAll o first, el programa luego debe eliminar todos los tramites asociados
        
        GuardarTodos(expedientes);//reescribo la lista solo con los expedientes que sirven
    }

    public Expediente ObtenerPorId(Guid id)
    {//esto deberia retornar el expediente con fecha mas reciente
        Expediente? expedienteEncontrado = null;
        var expedientes = BuscarTodos();
        foreach (Expediente expediente in expedientes)
        {
            if (expediente.Id == id)//tengo que ver si se repiten los id luego
            {
                if (expedienteEncontrado == null || expediente.FechaUltimaModificacion > expedienteEncontrado.FechaUltimaModificacion)
                {
                    expedienteEncontrado = expediente;
                }
            }
        }
        if(expedienteEncontrado != null)
        {
            return expedienteEncontrado;
        }
        throw new DominioException("Expediente no encontrado.");
    }
    

    private IEnumerable<Expediente> BuscarTodos()
    {
        List<Expediente> expedientes = new List<Expediente>();
        if (File.Exists(rutaArchivo))
        {
            var lineas = File.ReadAllLines(rutaArchivo);
            foreach (var linea in lineas)
            {
                var partes = linea.Split(';');
                if (partes.Length == 7)
                {   
                    expedientes.Add(Expediente.Reconstruir
                    (
                        Guid.Parse(partes[0]),
                        new CaratulaExpedientes(partes[1]),
                        DateTime.Parse(partes[2]),
                        DateTime.Parse(partes[3]),
                        Guid.Parse(partes[4]),
                        Enum.Parse<EstadoExpedientes>(partes[5])
                    ));};
                }
            }
        return expedientes;
    }
    private void GuardarTodos(List<Expediente> expedientes)
    {
        using (StreamWriter sw = new StreamWriter(rutaArchivo, false))
        {
            foreach (var expediente in expedientes)
            {
                sw.WriteLine($"{expediente.Id};{expediente.Caratula};{expediente.FechaCreacion};{expediente.FechaUltimaModificacion};{expediente.UsuarioUltimoCambio}");
            }
        }
    }
}