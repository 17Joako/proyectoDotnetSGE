public record class AgregarExpedienteRequest(
    CaratulaExpedientes Caratula, DateTime FechaCracion, Guid IdUsuario
);
public record class EliminarExpedienteRequest(
    Guid IdExpediente
);
public record class CambiarEstadoRequest(
    Guid IdUsuario,Guid IdExpediente, EstadoExpedientes NuevoEstado
);
public record class ModificarCaratulaRequest(
   Guid IdUsuario, Guid IdExpediente, CaratulaExpedientes Caratula, DateTime FechaDeCambio
);
public record class ObtenerExpedientePorIdRequest(Guid IdExpediente);

public record class ObtenerExpedientePorIdResponse(
    Expediente Expediente, IEnumerable<Tramite> Tramites
);  
public record class ListarExpedientesResponse(IEnumerable<Expediente> Expedientes);

public record class AgregarTramiteRequest(Guid UsuarioID, Guid ExpedienteID, ContenidoTramite Contenido);

public record class EliminarTramiteRequest(Guid UsuarioID, Guid Id);
public record class ModificarTramiteRequest(Guid id, ContenidoTramite nuevoContenido, EtiquetaTramites nuevaEtiqueta, Guid nuevoExpedienteId, Guid usuarioId);

public record class ListarTramitesResponse(IEnumerable<Tramite> Tramites);

public record class TramitesPorExpedienteRequest(Guid IdExpediente);