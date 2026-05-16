# SGE.Net


# Tutorial para usar git/github
# ¿Donde se escriben los comandos?
abro una nueva terminal de command prompt y ahí escribo todos los comandos que necesito

# ¿Como hacer pull?.
git pull

# ¿Como stageo los cambio que hice?.
git add .

# ¿Como commiteo los cambios?.
git commit -m "Agregue Program.cs para sge.consola"

# ¿Como hacer push?.
git push //solamente si la rama (branch) en la que estoy trabajando ya existe en el repositorio en github
git push -u origin/main[nombre de la rama a la que se conecta] miBranchJ[nombre de la rama en la que estoy trabajando] //para cuando la rama en la que trabajo no existe en el repositorio

# ¿Como hacer una nueva branch y como cambiar de branch?.
git branch //muestra todas las ramas y marca la rama en la que se está trabajando
git branch miBranchJ[nombre de nueva rama] //crea una nueva rama con ese nombre
git switch [nombre de rama] //cambia a la rama específicada

# ¿Cuál es el orden de los comandos?.
git switch main -> git pull -> git switch miBranch -> [hago todos los cambios que quiera] -> ctrl+s -> git add . -> git commit -m "[mensaje de commit preferiblemente explicativo]" -> git push/git push -u origin [rama]

# Forma de trabajo
cada uno va a usar su rama