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