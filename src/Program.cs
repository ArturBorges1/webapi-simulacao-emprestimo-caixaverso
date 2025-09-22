using Serilog;
using Caixaverso.Backend.CrossCutting.Logging;
using Caixaverso.Backend.CrossCutting.Swagger;
using Caixaverso.Backend.CrossCutting.Versioning;
using Caixaverso.Backend.CrossCutting.Validation;
using Caixaverso.Backend.CrossCutting.Exceptions;
using Caixaverso.Backend.CrossCutting.Database;
using Caixaverso.Backend.Application.Interfaces;
using Caixaverso.Backend.Application.UseCase;
using Caixaverso.Backend.Domain.Interfaces;
using Caixaverso.Backend.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

//Logging com Serilog
SerilogConfiguration.ConfigureSerilog();
builder.Host.UseSerilog();

//Documentação Swagger
builder.Services.AddSwaggerDocumentation(builder.Configuration);

// Versionamento da API
builder.Services.AddApiVersioningConfiguration();

//OpenAPI para compatibilidade com ferramentas externas
builder.Services.AddOpenApi();

//Controllers da API
builder.Services.AddControllers();

//FluentValidation
builder.Services.AddFluentValidationSetup();

//SQLite
builder.Services.AddSqlite(builder.Configuration);

builder.Services.AddScoped<IProdutoListarTodosUseCase, ProdutoListarTodosUseCase>();
builder.Services.AddScoped<IProdutoCriarUseCase, ProdutoCriarUseCase>();
builder.Services.AddScoped<IProdutoObterPorIdUseCase, ProdutoObterPorIdUseCase>();
builder.Services.AddScoped<IProdutoAtualizarUseCase, ProdutoAtualizarUseCase>();
builder.Services.AddScoped<IProdutoDeletarUseCase, ProdutoDeletarUseCase>();
builder.Services.AddScoped<ISimulacaoEmprestimoUseCase, SimulacaoEmprestimoUseCase>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();


var app = builder.Build();

//Ativa a interface do swagger para ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDocumentation(builder.Configuration);
}
app.MapOpenApi();

//Tratamento de exceções
app.UseGlobalExceptionHandling();

//Roteamento
app.UseRouting();

//Mapeamento para endpoints dos controllers
app.MapControllers();

app.UseHttpsRedirection();

app.Run();
