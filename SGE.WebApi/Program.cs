using SGE.Infraestructura;
using SGE.WebApi;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<ManejadorDeExceptionsGlobales>();

var connectionString = builder.Configuration.GetConnectionString("SGEDb");
builder.Services.AddDbContext<SgeContext>(options =>
    options.UseSqlite(connectionString));

//repositorios
    builder.Services.AddScoped<IExpedienteRepository, ExpedienteRepository>();
    builder.Services.AddScoped<ITramiteRepository, TramiteRepository>();
    builder.Services.AddScoped<IUnidadDeTrabajo, UnidadDeTrabajoRepository>();

//casos de uso
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
// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();*/

app.MapGet("/", () => "funciona");
SgeContext.Inicializar();

app.Run();