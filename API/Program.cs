using Dados.Contexto;
using Dados.Interfaces;
using Dados.Repositorios;
using Microsoft.EntityFrameworkCore;
using Servicos.Configuracoes;
using Servicos.Interfaces;
using Servicos.Servicos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<WeatherApiConfiguracao>(
    builder.Configuration.GetSection("WeatherApi"));

builder.Services.AddDbContext<PrevisaoDoTempoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PrevisaoTempoConnection")));

builder.Services.AddScoped<IClimaService, ClimaService>();
builder.Services.AddScoped<IPrevisaoService, PrevisaoService>();
builder.Services.AddScoped<IHistoricoService, HistoricoService>();

builder.Services.AddScoped<IClimaRepository, ClimaRepository>();
builder.Services.AddScoped<IPrevisaoRepository, PrevisaoRepository>();
builder.Services.AddScoped<IHistoricoRepository, HistoricoRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();