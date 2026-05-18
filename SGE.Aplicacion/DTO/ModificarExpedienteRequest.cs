public record class ModificarExpedienteRequest(
    Guid Id, DateTime FechaUltimaModificacion, Guid UsuarioUltimoCambio, DateTime FechaCreacion
);