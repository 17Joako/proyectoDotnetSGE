// URL base de la API
const API_URL = '/';

// Variables globales
let token = null;
let currentUser = null;

// Ejecutar cuando el documento cargue
document.addEventListener('DOMContentLoaded', () => {
    verificarToken();
    configurarEventos();
    cargarDashboard();
});

// Verificar si existe token válido
function verificarToken() {
    token = localStorage.getItem('token');
    
    if (!token) {
        window.location.href = '/login.html';
        return;
    }
}

// Configurar eventos de navegación
function configurarEventos() {
    // Navbar
    document.getElementById('navExpedientes').addEventListener('click', (e) => {
        e.preventDefault();
        mostrarSeccion('expedientes');
    });
    
    document.getElementById('navTramites').addEventListener('click', (e) => {
        e.preventDefault();
        mostrarSeccion('tramites');
    });
    
    document.getElementById('navUsuarios').addEventListener('click', (e) => {
        e.preventDefault();
        mostrarSeccion('usuarios');
    });
    
    document.getElementById('btnLogout').addEventListener('click', cerrarSesion);
    
    // Botones principales
    document.getElementById('btnNuevoExpediente').addEventListener('click', () => abrirModalExpediente());
    document.getElementById('btnNuevoTramite').addEventListener('click', () => abrirModalTramite());
    document.getElementById('btnNuevoUsuario').addEventListener('click', () => abrirModalUsuario());
    
    // Modal
    document.querySelector('.close-modal').addEventListener('click', cerrarModal);
    document.getElementById('modal').addEventListener('click', (e) => {
        if (e.target.id === 'modal') cerrarModal();
    });
}

// Cambiar sección visible
function mostrarSeccion(seccion) {
    // Ocultar todas las secciones
    document.querySelectorAll('.section').forEach(s => s.classList.add('hidden'));
    
    // Mostrar la sección solicitada
    switch(seccion) {
        case 'expedientes':
            document.getElementById('expedientesSection').classList.remove('hidden');
            cargarExpedientes();
            break;
        case 'tramites':
            document.getElementById('tramitesSection').classList.remove('hidden');
            cargarTramites();
            break;
        case 'usuarios':
            document.getElementById('usuariosSection').classList.remove('hidden');
            cargarUsuarios();
            break;
        default:
            document.getElementById('dashboardSection').classList.remove('hidden');
    }
}

// ===== DASHBOARD =====
async function cargarDashboard() {
    try {
        // Cargar conteos
        const expedientes = await hacerPeticion('GET', 'expedientes');
        const tramites = await hacerPeticion('GET', 'tramites');
        const usuarios = await hacerPeticion('GET', 'usuarios');
        
        document.getElementById('countExpedientes').textContent = expedientes.length || 0;
        document.getElementById('countTramites').textContent = tramites.length || 0;
        document.getElementById('countUsuarios').textContent = usuarios.length || 0;
    } catch (error) {
        console.error('Error al cargar dashboard:', error);
    }
}

// ===== EXPEDIENTES =====
async function cargarExpedientes() {
    try {
        const expedientes = await hacerPeticion('GET', 'expedientes');
        const content = document.getElementById('expedientesContent');
        
        if (expedientes.length === 0) {
            content.innerHTML = '<p>No hay expedientes registrados.</p>';
            return;
        }
        
        let html = '<div class="table-container"><table><thead><tr><th>ID</th><th>Número</th><th>Estado</th><th>Acciones</th></tr></thead><tbody>';
        
        expedientes.forEach(exp => {
            html += `
                <tr>
                    <td>${exp.id}</td>
                    <td>${exp.numero || 'N/A'}</td>
                    <td>${exp.estado || 'N/A'}</td>
                    <td>
                        <div class="action-buttons">
                            <button class="btn-secondary" onclick="abrirModalExpediente(${exp.id})">Editar</button>
                            <button class="btn-danger" onclick="eliminarExpediente(${exp.id})">Eliminar</button>
                        </div>
                    </td>
                </tr>
            `;
        });
        
        html += '</tbody></table></div>';
        content.innerHTML = html;
    } catch (error) {
        console.error('Error al cargar expedientes:', error);
        document.getElementById('expedientesContent').innerHTML = '<p class="error">Error al cargar los expedientes.</p>';
    }
}

function abrirModalExpediente(id = null) {
    const modal = document.getElementById('modal');
    const modalBody = document.getElementById('modalBody');
    
    const titulo = id ? 'Editar Expediente' : 'Nuevo Expediente';
    
    modalBody.innerHTML = `
        <h3>${titulo}</h3>
        <form id="formExpediente">
            <div class="form-group">
                <label for="expNumero">Número:</label>
                <input type="text" id="expNumero" required>
            </div>
            <div class="form-group">
                <label for="expEstado">Estado:</label>
                <select id="expEstado" required>
                    <option value="">Seleccionar estado</option>
                    <option value="Abierto">Abierto</option>
                    <option value="Cerrado">Cerrado</option>
                    <option value="En Trámite">En Trámite</option>
                </select>
            </div>
            <div class="form-actions">
                <button type="submit" class="btn-primary">Guardar</button>
                <button type="button" class="btn-secondary" onclick="cerrarModal()">Cancelar</button>
            </div>
        </form>
    `;
    
    document.getElementById('formExpediente').addEventListener('submit', (e) => {
        e.preventDefault();
        guardarExpediente(id);
    });
    
    modal.classList.remove('hidden');
}

async function guardarExpediente(id) {
    const numero = document.getElementById('expNumero').value;
    const estado = document.getElementById('expEstado').value;
    
    try {
        if (id) {
            await hacerPeticion('PUT', `expedientes/${id}`, { numero, estado });
        } else {
            await hacerPeticion('POST', 'expedientes', { numero, estado });
        }
        
        cerrarModal();
        cargarExpedientes();
    } catch (error) {
        console.error('Error al guardar expediente:', error);
        alert('Error al guardar el expediente');
    }
}

async function eliminarExpediente(id) {
    if (!confirm('¿Estás seguro de que deseas eliminar este expediente?')) return;
    
    try {
        await hacerPeticion('DELETE', `expedientes/${id}`);
        cargarExpedientes();
    } catch (error) {
        console.error('Error al eliminar expediente:', error);
        alert('Error al eliminar el expediente');
    }
}

// ===== TRÁMITES =====
async function cargarTramites() {
    try {
        const tramites = await hacerPeticion('GET', 'tramites');
        const content = document.getElementById('tramitesContent');
        
        if (tramites.length === 0) {
            content.innerHTML = '<p>No hay trámites registrados.</p>';
            return;
        }
        
        let html = '<div class="table-container"><table><thead><tr><th>ID</th><th>Descripción</th><th>Estado</th><th>Acciones</th></tr></thead><tbody>';
        
        tramites.forEach(trm => {
            html += `
                <tr>
                    <td>${trm.id}</td>
                    <td>${trm.descripcion || 'N/A'}</td>
                    <td>${trm.estado || 'N/A'}</td>
                    <td>
                        <div class="action-buttons">
                            <button class="btn-secondary" onclick="abrirModalTramite(${trm.id})">Editar</button>
                            <button class="btn-danger" onclick="eliminarTramite(${trm.id})">Eliminar</button>
                        </div>
                    </td>
                </tr>
            `;
        });
        
        html += '</tbody></table></div>';
        content.innerHTML = html;
    } catch (error) {
        console.error('Error al cargar trámites:', error);
        document.getElementById('tramitesContent').innerHTML = '<p class="error">Error al cargar los trámites.</p>';
    }
}

function abrirModalTramite(id = null) {
    const modal = document.getElementById('modal');
    const modalBody = document.getElementById('modalBody');
    
    const titulo = id ? 'Editar Trámite' : 'Nuevo Trámite';
    
    modalBody.innerHTML = `
        <h3>${titulo}</h3>
        <form id="formTramite">
            <div class="form-group">
                <label for="trmDescripcion">Descripción:</label>
                <textarea id="trmDescripcion" required></textarea>
            </div>
            <div class="form-group">
                <label for="trmEstado">Estado:</label>
                <select id="trmEstado" required>
                    <option value="">Seleccionar estado</option>
                    <option value="Pendiente">Pendiente</option>
                    <option value="En Proceso">En Proceso</option>
                    <option value="Completado">Completado</option>
                </select>
            </div>
            <div class="form-actions">
                <button type="submit" class="btn-primary">Guardar</button>
                <button type="button" class="btn-secondary" onclick="cerrarModal()">Cancelar</button>
            </div>
        </form>
    `;
    
    document.getElementById('formTramite').addEventListener('submit', (e) => {
        e.preventDefault();
        guardarTramite(id);
    });
    
    modal.classList.remove('hidden');
}

async function guardarTramite(id) {
    const descripcion = document.getElementById('trmDescripcion').value;
    const estado = document.getElementById('trmEstado').value;
    
    try {
        if (id) {
            await hacerPeticion('PUT', `tramites/${id}`, { descripcion, estado });
        } else {
            await hacerPeticion('POST', 'tramites', { descripcion, estado });
        }
        
        cerrarModal();
        cargarTramites();
    } catch (error) {
        console.error('Error al guardar trámite:', error);
        alert('Error al guardar el trámite');
    }
}

async function eliminarTramite(id) {
    if (!confirm('¿Estás seguro de que deseas eliminar este trámite?')) return;
    
    try {
        await hacerPeticion('DELETE', `tramites/${id}`);
        cargarTramites();
    } catch (error) {
        console.error('Error al eliminar trámite:', error);
        alert('Error al eliminar el trámite');
    }
}

// ===== USUARIOS =====
async function cargarUsuarios() {
    try {
        const usuarios = await hacerPeticion('GET', 'usuarios');
        const content = document.getElementById('usuariosContent');
        
        if (usuarios.length === 0) {
            content.innerHTML = '<p>No hay usuarios registrados.</p>';
            return;
        }
        
        let html = '<div class="table-container"><table><thead><tr><th>ID</th><th>Usuario</th><th>Email</th><th>Acciones</th></tr></thead><tbody>';
        
        usuarios.forEach(usr => {
            html += `
                <tr>
                    <td>${usr.id}</td>
                    <td>${usr.usuario || 'N/A'}</td>
                    <td>${usr.email || 'N/A'}</td>
                    <td>
                        <div class="action-buttons">
                            <button class="btn-secondary" onclick="abrirModalUsuario(${usr.id})">Editar</button>
                            <button class="btn-danger" onclick="eliminarUsuario(${usr.id})">Eliminar</button>
                        </div>
                    </td>
                </tr>
            `;
        });
        
        html += '</tbody></table></div>';
        content.innerHTML = html;
    } catch (error) {
        console.error('Error al cargar usuarios:', error);
        document.getElementById('usuariosContent').innerHTML = '<p class="error">Error al cargar los usuarios.</p>';
    }
}

function abrirModalUsuario(id = null) {
    const modal = document.getElementById('modal');
    const modalBody = document.getElementById('modalBody');
    
    const titulo = id ? 'Editar Usuario' : 'Nuevo Usuario';
    
    modalBody.innerHTML = `
        <h3>${titulo}</h3>
        <form id="formUsuario">
            <div class="form-group">
                <label for="usrUsuario">Usuario:</label>
                <input type="text" id="usrUsuario" required>
            </div>
            <div class="form-group">
                <label for="usrEmail">Email:</label>
                <input type="email" id="usrEmail" required>
            </div>
            <div class="form-group">
                <label for="usrPassword">Contraseña:</label>
                <input type="password" id="usrPassword" ${id ? '' : 'required'}>
                ${id ? '<small>Dejar vacío para mantener la contraseña actual</small>' : ''}
            </div>
            <div class="form-actions">
                <button type="submit" class="btn-primary">Guardar</button>
                <button type="button" class="btn-secondary" onclick="cerrarModal()">Cancelar</button>
            </div>
        </form>
    `;
    
    document.getElementById('formUsuario').addEventListener('submit', (e) => {
        e.preventDefault();
        guardarUsuario(id);
    });
    
    modal.classList.remove('hidden');
}

async function guardarUsuario(id) {
    const usuario = document.getElementById('usrUsuario').value;
    const email = document.getElementById('usrEmail').value;
    const password = document.getElementById('usrPassword').value;
    
    const data = { usuario, email };
    if (password) data.password = password;
    
    try {
        if (id) {
            await hacerPeticion('PUT', `usuarios/${id}`, data);
        } else {
            await hacerPeticion('POST', 'usuarios', data);
        }
        
        cerrarModal();
        cargarUsuarios();
    } catch (error) {
        console.error('Error al guardar usuario:', error);
        alert('Error al guardar el usuario');
    }
}

async function eliminarUsuario(id) {
    if (!confirm('¿Estás seguro de que deseas eliminar este usuario?')) return;
    
    try {
        await hacerPeticion('DELETE', `usuarios/${id}`);
        cargarUsuarios();
    } catch (error) {
        console.error('Error al eliminar usuario:', error);
        alert('Error al eliminar el usuario');
    }
}

// ===== FUNCIONES AUXILIARES =====
async function hacerPeticion(metodo, endpoint, datos = null) {
    const opciones = {
        method: metodo,
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        }
    };
    
    if (datos) {
        opciones.body = JSON.stringify(datos);
    }
    
    const response = await fetch(`${API_URL}${endpoint}`, opciones);
    
    if (response.status === 401) {
        // Token expirado
        cerrarSesion();
        return;
    }
    
    if (!response.ok) {
        throw new Error(`Error: ${response.status} ${response.statusText}`);
    }
    
    // Algunos endpoints pueden no devolver contenido
    if (response.status === 204) {
        return null;
    }
    
    return await response.json();
}

function cerrarModal() {
    document.getElementById('modal').classList.add('hidden');
    document.getElementById('modalBody').innerHTML = '';
}

function cerrarSesion() {
    localStorage.removeItem('token');
    window.location.href = '/login.html';
}
