# SGE.Net
Sistema de Gestión de Expedientes hecho en .Net

# Instrucciones de uso
    Para probar nuestro sistema hemos cedido algunos usuarios de prueba, un administrador, quien posee todos los permisos, y otros 4 usuarios con una variedad de permisos para probar las funcionalidades y los límites del sistema que hemos creado. Solicitamos no borrar ninguno de los usuarios de prueba otorgados.

    Para comenzar a utilizar nuestro Sistema de Gestión de Expedientes debe ejecutar con el comando dotnet run --project SGE.WebApi, 
    estando en la carpeta del proyecto, una vez haya iniciado debe entrar al siguiente link: http://localhost:5299/scalar/v1#tag/expedientes. 
    Una vez hecho esto, en la página a la que ha sido redirigido debe loggearse con algún usuario ya existente, o registrar uno nuevo 
    (se recomienda probar con los usuarios de prueba otorgados).
    
    Para loggearse con un usario existente debe dirigirse a la funcionalidad login de usuarios, 
    donde ingresando mail y contraseña en las áreas indicadas puede recibir su token de sesión, 
    el cuál debe guardar puesto que lo necesitará para realizar cualquiera de las acciones que el sistema soporta, y que tenga permiso¹ 
    (Se espera que reciba un código "200 OK" que indica que no hubo errores al realizar la operación, y en la sección de "Body" recibe el token JWT que previamente se ha indicado guardar).

    Para registrarse con un nuevo usuario debe dirigirse a la funcionalidad registrar de usuarios, 
    deberá ingresar nombre, email, contraseña y los permisos que tiene siguiendo la siguiente lógica: 
    0= alta de expedientes; 1= baja de expedientes; 2= modificación de expedientes; 3= alta de trámites; 4= baja de trámites; 5= modificación de trámites. 
    (Si no hubo ningún inconveniente deberá recibir nuevamente el código 200 OK y debajo el mensaje "Usuario registrado"). 
    Luego de haberse registrado deberá iniciar sesión para obtener el token JWT.

    Para agilizar la explicación vamos a dividir las funcionalidades en 4 tipos: POST, PUT, GET, DEL. 
    En la propia interfaz será visible de que tipo es cada funcionalidad. Las funcionalidades del mismo tipo mantendrán una estructura similar.

    Tipo POST y PUT: un Header donde se deberá crear la fila de nombre "Authorization" (en la casilla de la izquierda), 
    completar con "bearer" (en la casilla de la derecha) seguido del token recibido al momento de iniciar sesión, 
    un body de entrada donde se deberán rellenar los datos especificados en cada caso 
    si han habido complicaciones dará como salida un código 200 indicando que se pudo realizar la operación, 
    seguido de un mensaje confirmando esto mismo ("Usuario registrado", "Expediente agregado", etc.), con la única excepción del registrar que no requiere un token, 
    y el login que su mensaje de confirmación es el token de autenticación JWT.

    Tipo GET: como en el tipo anterior tendrá un Header dónde deberá proceder de la misma manera, 
    y en algunas funcionalidades de este tipo deberá proporcionar un ID en la zona marcada como Variables, el ID que deba proporcionar variará según lo que haga la funcionalidad 
    pero en todos los casos está especificado. Como salida tendrá (nuevamente si no hubieron inconvenientes un código 200) un body detallando lo solicitado, por ejemplo, 
    si utiliza la funcionalidad ListarTramites, recibirá una lista de todos los trámites, con sus datos en el siguiente orden: 
    ID del trámite, ID del expediente dónde se encuentra, la etiqueta del trámite, el contenido, la fecha de creación, la fecha de última modificación y el usuario que realizó el último cambio.
    
    Tipo DEL: nuevamente deberá rellenar el Header de la misma manera. Todas las funcionalidades de tipo DEL requieren de un ID que se deberá proporcionar en la sección de Variables. 
    Como salida tendrán el código y el mensaje de confirmación.

    Algunos códigos que pueden aparecer cuándo la operación no pudo realizarse con éxito:
    400: Error presentado en la capa de dominio
    401: No se ha proporcionado ningún token de autenticación.
    403: El usuario no posee permiso.
    404: Entidad no encontrada.
    500: Error interno del servidor.

    

    ¹ Hemos decidido que incluso las funcionalidades que no requieren ningún permiso concreto, igualmente requieran una autorización, 
    esto para evitar que usuarios no registrados puedan acceder a funcionalidades como listar los expedientes o los trámites, 
    los cuáles pueden llegar a tener información sensible.

# Usuarios de prueba
    Usuario Administrador:
        nombre: Juani
        correo: juani@gmail.com
        contraseña: admin987
    
    Usuario 1: Puede dar de alta, baja y modificar tramites
        nombre: Finn
        correo: finn@gmail.com
        contraseña: usuario111

    Usuario 2: Puede dar de alta y modificar expedientes
        nombre: Lucho
        correo: lucho@gmail.com
        contraseña: usuario222

    Usuario 3: Tiene todos los permisos pero no es administrador (puede hacer cualquier cosa con los expedientes y los tramites pero nada con los usuarios)
        nombre: Joako
        correo: joako@gmail.com
        contraseña: usuario333

    Usuario 4: No tiene ningún permiso
        nombre: Bauti
        correo: bauti@gmail.com
        contraseña: usuario444