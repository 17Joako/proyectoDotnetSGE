using SGE.Infraestructura;
using SGE.WebApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<ManejadorDeExceptionsGlobales>();

var connectionString = builder.Configuration.GetConnectionString("SGEDb") ?? "Data Source=sge.db";
builder.Services.AddDbContext<SgeContext>(options =>
    options.UseSqlite(connectionString));

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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
    builder.Services.AddAuthorization();

//repositorios
    builder.Services.AddScoped<IExpedienteRepository, ExpedienteRepository>();
    builder.Services.AddScoped<ITramiteRepository, TramiteRepository>();
    builder.Services.AddScoped<IUnidadDeTrabajo, UnidadDeTrabajoRepository>();

//casos de uso
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

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseExceptionHandler();
app.UseAuthentication();
app.UseAuthorization();
// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();*/

app.MapGet("/", () => "ARRRRRANCAAAAAAAAAAA");
SgeContext.Inicializar();

app.Run();