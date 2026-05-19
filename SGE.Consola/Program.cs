public class Program
{
    public static void Main(string[] args)
    {
        Guid idUsuarioActual = Guid.NewGuid(); // Simula un ID de usuario actual
        Console.WriteLine("Bienvenido al Sistema de Gestión de Expedientes (SGE)");
        Console.WriteLine($"ID de Usuario actual: {idUsuarioActual}");
        
        // instancio los repositorios que voy a necesitar para los distintos casos de uso
        ExpedienteTxtRepository expedienteTxtRepository = new ExpedienteTxtRepository();
        TramiteTxtRepository tramiteTxtRepository = new TramiteTxtRepository();
        AutorizacionProvisionalService autorizacionService = new AutorizacionProvisionalService();

        int opcion = -1;
        while (opcion != 9)
        {
            Console.WriteLine("Seleccione el numero de la opcion que desea realizar");
            Console.WriteLine("Ingrese 0 si desea dar de alta un expediente"); // CdU 1: dar de alta de un expediente
            Console.WriteLine("Ingrese 1 si desea dar de baja un expediente"); // CdU 2: dar de baja de un expediente
            Console.WriteLine("Ingrese 2 si desea modificar un expediente"); // CdU 3: modificar un expediente
            Console.WriteLine("Ingrese 3 si desea modificar el estado de un expediente"); // CdU 4: modificar el estado de un expediente
            Console.WriteLine("Ingrese 4 si desea modificar la caratula de un expediente"); // CdU 5: modificar la caratula de un expediente
            Console.WriteLine("Ingrese 5 si desea dar de alta un tramite"); // CdU 6: dar de alta de un tramite
            Console.WriteLine("Ingrese 6 si desea dar de baja un tramite"); // CdU 7: dar de baja de un tramite
            Console.WriteLine("Ingrese 7 si desea modificar un tramite"); // CdU 8: modificar un tramite
            Console.WriteLine("Ingrese 8 si desea cambiar el usuario actual"); // CdU 9: cambiar de usuario
            Console.WriteLine("Ingrese 9 si desea salir del programa"); // CdU 10: salir del programa
            string? opcionst = Console.ReadLine(); // lee la opcion ingresada por el usuario
            if (opcionst != null && int.TryParse(opcionst, out opcion)) // comprueba si se ingresó null y si se puede convertir a int
            {
                // cosas para simular algunos casos
                CaratulaExpedientes nuevaCaratula = new CaratulaExpedientes("Expediente de demostración"); // caratula genérica para probar la funcionalidad del programa
                ContenidoTramite nuevoContenido = new ContenidoTramite("Contenido de demostración"); // Simula un nuevo contenido para el trámite
                Expediente expediente = new Expediente(nuevaCaratula, DateTime.Now); // expediente genérico para probar la funcionalidad del programa
                Tramite tramite = new Tramite(Guid.NewGuid(), nuevoContenido);
                TramiteRequest tramiteRequest = new TramiteRequest(idUsuarioActual, Guid.NewGuid(), Guid.NewGuid(), Etiqueta.EscritoPresentado, nuevoContenido, DateTime.Now,DateTime.Now, idUsuarioActual);
                ModificarTramiteRequest modificarTramiteRequest = new ModificarTramiteRequest(tramite, idUsuarioActual);
                AgregarExpedienteRequest agregarExpedienteRequest = new AgregarExpedienteRequest(idUsuarioActual, nuevaCaratula, DateTime.Now, DateTime.Now, idUsuarioActual);
                ActualizacionEstadoExpedienteService actualizacion_estado_expediente_service = new ActualizacionEstadoExpedienteService(expedienteTxtRepository, tramiteTxtRepository);
                // fin de cosas para simular algunos casos
                switch (opcion)
                {
                    case 0:
                        Console.WriteLine("Ingresó 0: Dar de alta un expediente");
                        ExpedienteAltaUseCase expedienteAltaUseCase = new ExpedienteAltaUseCase(expedienteTxtRepository, autorizacionService);
                        expedienteAltaUseCase.Ejecutar(agregarExpedienteRequest, idUsuarioActual);
                        break;
                    case 1:
                        Console.WriteLine("Ingresó 1: Dar de baja un expediente");
                        ExpedienteBajaUseCase expedienteBajaUseCase = new ExpedienteBajaUseCase(expedienteTxtRepository, tramiteTxtRepository, autorizacionService);
                        expedienteBajaUseCase.Ejecutar(agregarExpedienteRequest, idUsuarioActual);
                        break;
                    case 2:
                        Console.WriteLine("Ingresó 2: Modificar un expediente");
                        ModificarExpedienteUseCase modificarExpedienteUseCase = new ModificarExpedienteUseCase(expedienteTxtRepository, autorizacionService);
                        modificarExpedienteUseCase.Ejecutar(new ModificarExpedienteRequest(expediente, idUsuarioActual));
                        break;
                    case 3:
                        Console.WriteLine("Ingresó 3: Modificar el estado de un expediente");
                        CambiarEstadoExpedienteUseCase modificarEstadoExpedienteUseCase = new CambiarEstadoExpedienteUseCase(expedienteTxtRepository, autorizacionService);
                        EstadoExpedientes estado = EstadoExpedientes.ParaResolver; // Simula un nuevo estado para el expediente
                        modificarEstadoExpedienteUseCase.Ejecutar(new CambiarEstadoRequest(idUsuarioActual, Guid.NewGuid(), estado));
                        break;
                    case 4:
                        Console.WriteLine("Ingresó 4: Modificar la caratula de un expediente");
                        ModificarCaratulaExpedienteUseCase modificarCaratulaExpedienteUseCase = new ModificarCaratulaExpedienteUseCase(expedienteTxtRepository, autorizacionService);
                        modificarCaratulaExpedienteUseCase.Ejecutar(new ModificarCaratulaRequest(idUsuarioActual, Guid.NewGuid(), nuevaCaratula, DateTime.Now));
                        break;
                    case 5:
                        Console.WriteLine("Ingresó 5: Dar de alta un tramite");
                        TramiteAltaUseCase tramiteAltaUseCase = new TramiteAltaUseCase(tramiteTxtRepository, autorizacionService, actualizacion_estado_expediente_service);
                        tramiteAltaUseCase.Ejecutar(tramiteRequest);
                        break;
                    case 6:
                        Console.WriteLine("Ingresó 6: Dar de baja un tramite");
                        TramiteBajaUseCase tramiteBajaUseCase = new TramiteBajaUseCase(tramiteTxtRepository, autorizacionService, actualizacion_estado_expediente_service);
                        tramiteBajaUseCase.Ejecutar(tramiteRequest);
                        break;
                    case 7:
                        Console.WriteLine("Ingresó 7: Modificar un tramite");
                        ModificarTramiteUseCase tramiteModificarUseCase = new ModificarTramiteUseCase(tramiteTxtRepository, autorizacionService, actualizacion_estado_expediente_service);
                        tramiteModificarUseCase.Ejecutar(modificarTramiteRequest);
                        break;
                    case 8:
                        Console.WriteLine("Ingresó 8: Cambiar de usuario");
                        Console.WriteLine("Ingrese el nuevo ID de usuario (GUID):");
                        string? nuevoUsuarioIdStr = Console.ReadLine();
                        if (Guid.TryParse(nuevoUsuarioIdStr, out Guid nuevoUsuarioId))
                        {
                            idUsuarioActual = nuevoUsuarioId;
                        }
                        else
                        {
                            Console.WriteLine("ID de usuario no válido.");
                        }
                        break;
                    case 9:
                        Console.WriteLine("Ingresó 9: Salir del programa");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Opción no válida. Por favor ingrese una opción válida");
                opcionst = Console.ReadLine();
            }
        }
    }
}