using SGE.Infraestructura;
using SGE.WebApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Scalar.AspNetCore;
using SGE.WebApi.Endpoints.ExpedienteEndpoints;
using SGE.WebApi.Endpoints.TramiteEndpoints;
using SGE.WebApi.Endpoints.UsuariosEndpoint;

var builder = WebApplication.CreateBuilder(args);

// Agregar servicios
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

// CORS - Permitir solicitudes desde el frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Swagger/OpenAPI
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Repositorios
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IExpedienteRepository, ExpedienteRepository>();
builder.Services.AddScoped<ITramiteRepository, TramiteRepository>();
builder.Services.AddScoped<IUnidadDeTrabajo, UnidadDeTrabajoRepository>();

// Casos de uso
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

// Configurar middleware
app.UseExceptionHandler();

// Usar CORS
app.UseCors("AllowAll");

// Servir archivos estáticos (CSS, JS, imágenes, etc.)
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

// Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SGE API V1");
});

app.MapOpenApi();
app.MapScalarApiReference();

// Mapear endpoints
app.MapUsuarioEndpoints();
app.MapExpedienteEndpoints();
app.MapTramiteEndpoints();

// Inicializar base de datos
SgeContext.Inicializar();

app.Run();
