using ServicosBackground;
using Dados.Contexto;
using Dados.Interfaces;
using Dados.Repositorios;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);

// Configuração do Banco de Dados
builder.Services.AddDbContext<PrevisaoDoTempoDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PrevisaoTempoConnection")));

// Registro dos Repositórios
builder.Services.AddScoped<IClimaRepository, ClimaRepository>();
builder.Services.AddScoped<IPrevisaoRepository, PrevisaoRepository>();
builder.Services.AddScoped<IHistoricoRepository, HistoricoRepository>();

// Serviço em Background
builder.Services.AddHostedService<LimpaCacheService>();

var host = builder.Build();
host.Run();