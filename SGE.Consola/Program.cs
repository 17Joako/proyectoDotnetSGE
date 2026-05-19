public class Program
{
    public static void Main(string[] args)
    {
        // Crea una instancia de la clase Expediente
        Guid idUsuarioActual = Guid.NewGuid(); // Simula un ID de usuario actual
        Console.WriteLine("Bienvenido al Sistema de Gestión de Expedientes (SGE)");
        Console.WriteLine($"ID de Usuario actual: {idUsuarioActual}");
        int opcion = -1;
        while (opcion != 10)
        {
            Console.WriteLine("Seleccione el numero de la opcion que desea realizar");
            Console.WriteLine("Ingrese 0 si desea dar de alta un expediente"); // CdU 1: dar de alta de un expediente
            Console.WriteLine("Ingrese 1 si desea dar de baja un expediente"); // CdU 2: dar de baja de un expediente
            Console.WriteLine("Ingrese 2 si desea cambiar el estado del expediente"); // CdU 3: cambiar el estado de un expediente
            Console.WriteLine("Ingrese 3 si desea modificar un expediente"); // CdU 4: modificar un expediente
            Console.WriteLine("Ingrese 4 si desea modificar el estado de un expediente"); // CdU 5: modificar el estado de un expediente
            Console.WriteLine("Ingrese 5 si desea modificar la caratula de un expediente"); // CdU 6: modificar la caratula de un expediente
            Console.WriteLine("Ingrese 6 si desea dar de alta un tramite"); // CdU 7: dar de alta de un tramite
            Console.WriteLine("Ingrese 7 si desea dar de baja un tramite"); // CdU 8: dar de baja de un tramite
            Console.WriteLine("Ingrese 8 si desea modificar un tramite"); // CdU 9: modificar un tramite
            Console.WriteLine("Ingrese 9 si desea cambiar el usuario actual"); // CdU 10: cambiar de usuario
            Console.WriteLine("Ingrese 10 si desea salir del programa"); // CdU 11: salir del programa
            string? opcionst = Console.ReadLine();
            if (opcionst != null && int.TryParse(opcionst, out opcion))
            {
                switch (opcion)
                {
                    case 0:
                        Console.WriteLine("Ingresó 0: Dar de alta un expediente");
                        break;
                    case 1:
                        Console.WriteLine("Ingresó 1: Dar de baja un expediente");
                        break;
                    case 2:
                        Console.WriteLine("Ingresó 2: Cambiar el estado de un expediente");
                        break;
                    case 3:
                        Console.WriteLine("Ingresó 3: Modificar un expediente");
                        break;
                    case 4:
                        Console.WriteLine("Ingresó 4: Modificar el estado de un expediente");
                        break;
                    case 5:
                        Console.WriteLine("Ingresó 5: Modificar la caratula de un expediente");
                        break;
                    case 6:
                        Console.WriteLine("Ingresó 6: Dar de alta un tramite");
                        break;
                    case 7:
                        Console.WriteLine("Ingresó 7: Dar de baja un tramite");
                        break;
                    case 8:
                        Console.WriteLine("Ingresó 8: Modificar un tramite");
                        break;
                    case 9:
                        Console.WriteLine("Ingresó 9: Cambiar de usuario");
                        break;
                    case 10:
                        Console.WriteLine("Ingresó 10: Salir del programa");
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