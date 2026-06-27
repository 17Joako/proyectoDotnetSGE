// URL base de la API
const API_URL = '/';
console.log("ESTOY USANDO EL LOGIN.JS DE WWWROOT");

// Ejecutar cuando el documento cargue
document.addEventListener('DOMContentLoaded', () => {
    const loginForm = document.getElementById('loginForm');
    const registerForm = document.getElementById('registerForm');
    
    if (loginForm) {
        loginForm.addEventListener('submit', login);
    }
    
    if (registerForm) {
        registerForm.addEventListener('submit', registro);
    }
});

// Cambiar entre tabs de login y registro
function cambiarTab(tab) {
    const loginTab = document.getElementById('loginTab');
    const registerTab = document.getElementById('registerTab');
    const loginContent = document.getElementById('loginContent');
    const registerContent = document.getElementById('registerContent');
    
    if (tab === 'login') {
        loginTab.classList.add('active');
        registerTab.classList.remove('active');
        loginContent.classList.add('active');
        registerContent.classList.remove('active');
    } else {
        loginTab.classList.remove('active');
        registerTab.classList.add('active');
        loginContent.classList.remove('active');
        registerContent.classList.add('active');
    }
}

async function login(event) {
    event.preventDefault();

    const usuario = document.getElementById("usuario").value;
    const password = document.getElementById("password").value;
    const errorDiv = document.getElementById("error");

    errorDiv.innerHTML = "";

    try {
        const response = await fetch(`${API_URL}usuarios/login`, {
            method: "POST",
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                correoElectronico: usuario,
                contrasena: password
            })
        });

        if (!response.ok) {
            console.log("Status:", response.status);
            const texto = await response.text();
            console.log("Respuesta de error:", texto);
            errorDiv.textContent = "Error " + response.status + ": Usuario o contraseña incorrectos";
            return;
        }

        // Leemos la respuesta del servidor
        const data = await response.json();
        console.log("Respuesta completa del servidor:", data);

        // EXTRAER EL TOKEN DE MANERA SEGURA (Evita el [object Object])
        let token = null;

        if (typeof data === 'string') {
            // Si la API devuelve directamente el texto del token
            token = data;
        } else if (data && typeof data === 'object') {
            // Si la API devuelve un objeto, buscamos todas las variantes posibles de nombres
            token = data.token ?? data.Token ?? data.jwt ?? data.Jwt ?? data.accessToken ?? data.tokenTexto;
        }

        console.log("Token extraído:", token);

        if (!token) {
            errorDiv.textContent = "Error: El servidor no devolvió un formato de token reconocido.";
            console.error("No se pudo mapear el token desde:", data);
            return;
        }

        // Guardamos el string limpio del token en el almacenamiento local
        localStorage.setItem("token", token);
        
        alert("¡Sesión iniciada con éxito!");
        window.location.href = "/index.html";

    } catch (err) {
        console.error(err);
        errorDiv.innerHTML = "Error de conexión con el servidor";
    }
}

async function registro(event) {
    event.preventDefault();

    const nombre = document.getElementById("regNombre").value;
    const email = document.getElementById("regEmail").value;
    const password = document.getElementById("regPassword").value;
    const confirmPassword = document.getElementById("regConfirmPassword").value;
    const errorDiv = document.getElementById("registerError");

    // Validar que las contraseñas coincidan
    if (password !== confirmPassword) {
        errorDiv.textContent = "Las contraseñas no coinciden";
        errorDiv.classList.add("show");
        return;
    }

    // Validar longitud mínima de contraseña
    if (password.length < 6) {
        errorDiv.textContent = "La contraseña debe tener al menos 6 caracteres";
        errorDiv.classList.add("show");
        return;
    }

    try {
        const response = await fetch(`${API_URL}usuarios`, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                nombre: nombre,
                correoElectronico: email,
                contrasena: password,
                esAdministrador: false,
                permisosUsuario: []
            })
        });

        if (!response.ok) {
            const errorData = await response.json();
            errorDiv.textContent = errorData.detail || "Error al registrar. El correo puede estar en uso.";
            errorDiv.classList.add("show");
            return;
        }

        // Registro exitoso
        errorDiv.classList.remove("show");
        alert("Registro exitoso. Por favor inicia sesión.");
        
        // Limpiar formulario y cambiar a tab de login
        document.getElementById("registerForm").reset();
        cambiarTab('login');

    } catch (error) {
        console.error("Error en registro:", error);
        errorDiv.textContent = "Error al conectar con el servidor";
        errorDiv.classList.add("show");
    }
}