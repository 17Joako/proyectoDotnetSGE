// URL base de la API
const API_URL = '/';

// Ejecutar cuando el documento cargue
document.addEventListener('DOMContentLoaded', () => {
    const loginForm = document.getElementById('loginForm');
    if (loginForm) {
        loginForm.addEventListener('submit', login);
    }
});

async function login(event) {
    event.preventDefault();

    const usuario = document.getElementById("usuario").value;
    const password = document.getElementById("password").value;
    const errorDiv = document.getElementById("error");

    try {
        const response = await fetch(`${API_URL}login`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                usuario,
                password
            })
        });

        if (!response.ok) {
            errorDiv.textContent = "Usuario o contraseña incorrectos";
            errorDiv.classList.add("show");
            return;
        }

        const data = await response.json();
        
        // Guardar token en localStorage
        localStorage.setItem("token", data.token);
        
        // Redirigir a página principal
        window.location.href = "/index.html";

    } catch (error) {
        console.error("Error en login:", error);
        errorDiv.textContent = "Error al conectar con el servidor";
        errorDiv.classList.add("show");
    }
}
