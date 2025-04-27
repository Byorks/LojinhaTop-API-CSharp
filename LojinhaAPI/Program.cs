using LojinhaAPI.Infraestructure;
using LojinhaAPI.Infraestructure.Repositories;
using LojinhaAPI.Infraestructure.Repositories.Interfaces;
using LojinhaAPI.Services;
using LojinhaAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var assembly = Assembly.GetExecutingAssembly();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Nome do projeto pego de forma dinamica
    string xmlFile = $"{assembly.GetName().Name}.xml";
    string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    options.IncludeXmlComments(xmlPath);


});

// Injeção de dependència entre interface de repositório e implementação
// A partir disso, é possível acessar a implementação do repositório a partir da interface
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITypeUserRepository, TypeUserRepository>();

// Services
builder.Services.AddScoped<IUserServices, UserServices>();

// Configuração de Banco de Dados
builder.Services.AddDbContext<LojinhaDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
