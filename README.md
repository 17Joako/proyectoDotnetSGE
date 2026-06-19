# SGE.Net


# Tutorial para usar git/github
# ¿Donde se escriben los comandos?
abro una nueva terminal de command prompt y ahí escribo todos los comandos que necesito

# ¿Como hacer pull?.
git pull

# ¿Como stageo los cambio que hice?.
git add . // Para stagear todos los cambios realizados en todo el proyecto
git add [path al archivo] // Para stagear cambios de una clase o carpeta específicas

# ¿Como commiteo los cambios?.
git commit -m "Agregue Program.cs para sge.consola"

# ¿Como hacer push?.
git push //solamente si la rama (branch) en la que estoy trabajando ya existe en el repositorio en github
git push -u origin[nombre de la rama a la que se conecta] miBranch[nombre de la rama en la que estoy trabajando] //para cuando la rama en la que trabajo no existe en el repositorio

# ¿Como hacer una nueva branch y como cambiar de branch?.
git branch //muestra todas las ramas y marca la rama en la que se está trabajando

git branch [nombre de nueva rama] //crea una nueva rama con ese nombre
git switch [nombre de rama] //cambia a la rama específicada

git checkout -b [nombre de nueva rama] // Hace git branch [nombre de nueva rama] y git switch [nombre de rama] pero en una única línea

# ¿Cuál es el orden de los comandos?.
git switch main -> git pull -> git switch miBranch -> [hago todos los cambios que quiera] -> ctrl+s -> git add .[en lo posible especificar para evitar problemas]
-> git commit -m "[mensaje explicativo sobre los cambios realizados]" -> git push/git push -u origin [miBranch]

# ¿Como traer cambios de la main a mi rama?
git switch miBranch -> git pull origin main // Se debe hacer luego de haber pusheado cambios, o sin cambios hechos no guardados

# Forma de trabajo
Cada uno va a usar su rama y cuando desee guardará los cambios hechos en github
(recordar hacer cada intervalos de tiempo razonables, no a cada minimo cambio pero tampoco una vez al día)
y desde la página hace la pull request y merge de la rama propia con la main
# Como usar linq
    Consultas y Transformaciones
        Select()
        
        where: Filtra segun lo que pongas(por ej where (apellido=Morales) devolveria solo a Mario Joaquin Morales)

        OrderBy: ordena por un criterio dado, de forma ascendente 

        OrderByDescending:ordena por un criterio dado, de forma descendente

        Reverse: invierte el orden de la secuencia
    Sumas y Estadisticas
        Sum: calcula una suma

        Average: calcula un promedio

        Max: devuelve el maximo

        Min:devuelve el minimo

        Count: Hace lo mismo que lenght 
    No se que nombre poner
        First: devuelve el primer elemento de una secuencia, tira una excepcion si no existe

        FirstOrDefault:Igual que First, pero si no encuentra devuelve lo que le hayamos dicho

        Last: devuelve el ultimo elemento de la lista(no se que hace si no hay)

        SingleOrDefault: si hay un elemento, lo devuelve, si no hay ninguno, devuelve un valor por defecto, y si hay mas de 1 tira error

    Cuantificador
        All: Determina si todo cumplen la condicion

        Any: Determina si al menos 1 cumple la condicion
    Me quedo sola

     GroupBy: sirve para crear sub grupos dentro de un grupo mas grande ej:
            1.Fin|Cobannera|La Plata
            2.Bautista|Recalt|Ayacucho
            3.Joaquin|Morales|La Plata
        Si aplicamos la funcion agrupandolos por ciudad quedaria:
            1.Fin|Cobannera|La Plata
            2.Joaquin|Morales|La Plata
            3.Bautista|Recalt|Ayacucho
# Como usar sqlite 
0- Para crear la base de datos:
    context.Database.EnsureCreated()
    Este comando lee nuestras clases y crea una base de datos sqlite automaticamente con todas las tablas y relaciones
1- Para realizar consultas a la base de datos se utiliza:
    Todo lo de linq, por ej:
    context.Alumnos.Where(...)
    Aca tenemos que usar linq que vimos en clase
2- Para agregar algo a la base de datos se utiliza:
    context.Add(objeto)
3- Para eliminar algo de la base de datos:
    context.Remove(objeto)
4-Para persistir los datos:
    context.SaveChanges()

Al usar el paso 2 y 3, realizamos cambios locales en la base de datos que se persisten cuando usamos el paso 4, mientras tanto el archivo solo se modifica en memoria ram.