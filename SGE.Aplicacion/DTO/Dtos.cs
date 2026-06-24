public record class AgregarExpedienteRequest(
    CaratulaExpedientes Caratula, DateTime FechaCracion
);

public record class CambiarEstadoRequest(Guid UsuarioId,Guid Id, EstadoExpedientes NuevoEstado);
public record class EliminarExpedienteRequest(
    Guid Id
);

public record class ModificarCaratulaRequest(
   Guid UsuarioId, Guid Id, CaratulaExpedientes Caratula, DateTime FechaDeCambio
);

public record class obtenerExpedientePorIdRequest(Guid Id);

public record class obtenerExpedientePorIdResponse(Expediente Expediente, IEnumerable<Tramite> Tramites);  

public record class AgregarTramiteRequest(Guid UsuarioID, Guid ExpedienteID, ContenidoTramite Contenido);

public record class EliminarTramiteRequest(Guid UsuarioID, Guid Id);
public record class ModificarTramiteRequest(ContenidoTramite nuevoContenido, EtiquetaTramites nuevaEtiqueta, Guid nuevoExpedienteId, Guid usuarioId);

public record class ListarTramitesResponse(IEnumerable<Tramite> Tramites);
public record class ListarExpedientesResponse(IEnumerable<Expediente> Expedientes);

public record class TramitesPorExpedienteRequest(Guid IdExpediente);

//Empiezo los DTOS de usuario
//registrar usuario DTO
public record class RegistrarUsuarioRequest(string Nombre, string CorreoElectronico, string Contrasena, List<PermisoUsuarios> Permisos);

//Loguear DTO
public record class LoginRequest(string CorreoElectronico, string Contrasena);

public record class LoginResponse(Usuario Usuario);

public record class ListarUsuariosRequest(Guid UsuarioId);

public record class ModificarUsuarioRequest(string Nombre, string CorreoElectronico, string Contrasena);
//luego analizar 
public record class EliminarUsuarioRequest(Guid UsuarioId, Guid IdUsuarioAEliminar);

public record class ModificarPermisoRequest(Guid UsuarioId, Guid IdUsuarioAModificar, List<PermisoUsuarios> PermisosNuevos);