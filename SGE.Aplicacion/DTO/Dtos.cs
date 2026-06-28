public record class AgregarExpedienteRequest(
    CaratulaExpedientes Caratula
);
public record class EliminarExpedienteRequest(
    Guid IdExpediente
);
public record class CambiarEstadoRequest(
    Guid IdExpediente, EstadoExpedientes NuevoEstado
);
public record class ModificarCaratulaRequest(
   Guid IdExpediente, CaratulaExpedientes Caratula
);
public record class ObtenerExpedientePorIdRequest(Guid IdExpediente);

public record class ObtenerExpedientePorIdResponse(
    Expediente Expediente, IEnumerable<Tramite> Tramites
);  
public record class ListarExpedientesResponse(IEnumerable<Expediente> Expedientes);

public record class AgregarTramiteRequest(Guid ExpedienteID, ContenidoTramite Contenido);

public record class EliminarTramiteRequest(Guid Id);
public record class ModificarTramiteRequest(Guid id, ContenidoTramite nuevoContenido, EtiquetaTramites nuevaEtiqueta, Guid nuevoExpedienteId);

public record class ListarTramitesResponse(IEnumerable<Tramite> Tramites);

public record class TramitesPorExpedienteRequest(Guid IdExpediente);

//Empiezo los DTOS de usuario
//registrar usuario DTO
public record class RegistrarUsuarioRequest(string Nombre, string CorreoElectronico, string Contrasena,/*bool esAdministrador,*/List<PermisoUsuarios> permisosUsuario);
                                                        // saqué esto para que no aparezca en el scalar la posibilidad de un usuario que se registra pueda hacerse admin
//Loguear DTO
public record class LoginRequest(string CorreoElectronico, string Contrasena);

public record class LoginResponse(String token);

public record class ListarUsuariosRequest();

public record class ListarUsuarioResponse(IEnumerable<Usuario>? Usuarios);
public record class ModificarUsuarioRequest(string Nombre, string CorreoElectronico, string Contrasena);
//luego analizar 
public record class EliminarUsuarioRequest(Guid IdUsuarioAEliminar);

public record class ModificarPermisoRequest(Guid IdUsuarioAModificar, List<PermisoUsuarios> PermisosNuevos,bool esAdmin);