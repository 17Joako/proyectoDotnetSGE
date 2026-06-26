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

//Empiezo los DTOS de usuario
//registrar usuario DTO
public record class RegistrarUsuarioRequest(string Nombre, string CorreoElectronico, string Contrasena, List<PermisoUsuarios> Permisos);

//Loguear DTO
public record class LoginRequest(string CorreoElectronico, string Contrasena);

public record class LoginResponse(String token);

public record class ListarUsuariosRequest(Guid UsuarioId);

public record class ModificarUsuarioRequest(string Nombre, string CorreoElectronico, string Contrasena);
//luego analizar 
public record class EliminarUsuarioRequest(Guid UsuarioId, Guid IdUsuarioAEliminar);

public record class ModificarPermisoRequest(Guid UsuarioId, Guid IdUsuarioAModificar, List<PermisoUsuarios> PermisosNuevos);