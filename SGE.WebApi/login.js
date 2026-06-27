async function login(){
    // Mantenemos los IDs de tu HTML intactos
    const usuario = document.getElementById("usuario").value;
    const password = document.getElementById("password").value;

    // CORRECCIÓN 1: La "u" de /usuarios va en minúscula para coincidir con C#
    const response = await fetch("/usuarios/login", { 
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        // CORRECCIÓN 2: El JSON debe llevar las propiedades que espera LoginRequest
        body: JSON.stringify({
            correoElectronico: usuario, // Mapea a request.CorreoElectronico
            contrasena: password        // Mapea a request.Contrasena
        })
    });

    if(!response.ok){
        document.getElementById("error").innerHTML = "Usuario o contraseña incorrectos";
        return;
    }

    const data = await response.json();
    localStorage.setItem("token", data.token);
    window.location.href = "/index.html";
}