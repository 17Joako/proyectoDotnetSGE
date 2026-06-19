public record class ModificarCaratulaRequest(
   Guid UsuarioId, Guid Id, CaratulaExpedientes Caratula, DateTime FechaDeCambio
);