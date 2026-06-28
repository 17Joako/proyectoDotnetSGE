using SGE.Infraestructura;
using SGE.WebApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Scalar.AspNetCore; // Correcto
using SGE.WebApi.Endpoints.ExpedienteEndpoints;
using SGE.WebApi.Endpoints.TramiteEndpoints;
using SGE.WebApi.Endpoints.UsuariosEndpoint;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// --- 1. CONFIGURACIÓN DE SERVICIOS ---
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<ManejadorDeExceptionsGlobales>();

// Base de datos
var connectionString = builder.Configuration.GetConnectionString("SGEDb") ?? "Data Source=sge.db";
builder.Services.AddDbContext<SgeContext>(options =>
    options.UseSqlite(connectionString));

// Autenticación JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("Configuration 'Jwt:Key' is missing")
                )
            )
        };
    });

builder.Services.AddAuthorization();

// OpenAPI con configuración de Seguridad para Scalar
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Info.Title = "SGE API - Sistema de Gestión de Expedientes";
        document.Info.Version = "v1";
        return Task.CompletedTask;
    });
});

// Repositorios y Casos de Uso
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IExpedienteRepository, ExpedienteRepository>();
builder.Services.AddScoped<ITramiteRepository, TramiteRepository>();
builder.Services.AddScoped<IUnidadDeTrabajo, UnidadDeTrabajoRepository>();

builder.Services.AddScoped<RegistrarUsuarioUseCase>();
builder.Services.AddScoped<ModificarMisDatosUseCase>();
builder.Services.AddScoped<LoginUseCase>();
builder.Services.AddScoped<EliminarUsuarioUseCase>();
builder.Services.AddScoped<ListarUsuariosUseCase>();
builder.Services.AddScoped<ModificarPermisosUsuarioUseCase>();
builder.Services.AddScoped<IAutorizacionService, AutorizacionService>();
builder.Services.AddScoped<ActualizacionEstadoExpedienteService>();
builder.Services.AddScoped<ExpedienteAltaUseCase>();
builder.Services.AddScoped<ExpedienteBajaUseCase>();
builder.Services.AddScoped<CambiarEstadoExpedienteUseCase>();
builder.Services.AddScoped<ModificarCaratulaExpedienteUseCase>();
builder.Services.AddScoped<ListarExpedientesUseCase>();
builder.Services.AddScoped<ObtenerExpedientePorIdUseCase>();
builder.Services.AddScoped<TramiteAltaUseCase>();
builder.Services.AddScoped<TramiteBajaUseCase>();
builder.Services.AddScoped<ModificarTramiteUseCase>();
builder.Services.AddScoped<ListarTramitesUseCase>();
builder.Services.AddScoped<ListarTramitesPorExpedienteUseCase>();
// Construir la aplicación
var app = builder.Build();

// 1. Manejo de errores primero
app.UseExceptionHandler();

// 2. Configuración de Scalar y OpenAPI (Solo en desarrollo)
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); 
    app.MapScalarApiReference(options => 
    {
        options.WithTitle("SGE - Documentación de API")
               .WithTheme(ScalarTheme.Moon);
    });
}

// 3. Seguridad (Orden crítico)
app.UseAuthentication();
app.UseAuthorization();

// 4. Mapear tus endpoints de negocio
app.MapUsuarioEndpoints();
app.MapExpedienteEndpoints();
app.MapTramiteEndpoints();

// 5. Inicializar base de datos
using (var scope = app.Services.CreateScope())
{
    // Esto asegura que la DB se cree al arrancar
    SgeContext.Inicializar(); 
}

app.Run();