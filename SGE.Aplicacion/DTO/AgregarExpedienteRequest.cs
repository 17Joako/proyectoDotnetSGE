public record class AgregarExpedienteRequest(
    Guid Id, CaratulaExpedientes Caratula, DateTime FechaCracion, DateTime FechaUltimaModificacion, Guid UsuarioUltimoCambio
);