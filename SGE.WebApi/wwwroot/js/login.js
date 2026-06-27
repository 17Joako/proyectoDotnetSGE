document.getElementById('loginForm').addEventListener('submit', async function(e) {
    e.preventDefault();
    const usuario = document.getElementById("usuario").value;
    const password = document.getElementById("password").value;
    const errorDiv = document.getElementById("error");

    errorDiv.innerHTML = "";

    try {
        const response = await fetch('http://localhost:5299/login', { 
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ usuario, password })
        });

        if (!response.ok) {
            errorDiv.innerHTML = "Usuario o contraseña incorrectos";
            return;
        }

        const data = await response.json();
        localStorage.setItem("token", data.token);
        window.location.href = "./index.html";

    } catch (err) {
        console.error(err);
        errorDiv.innerHTML = "Error de conexión con el servidor";
    }
});