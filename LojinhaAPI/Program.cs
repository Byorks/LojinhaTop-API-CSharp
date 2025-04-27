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

// Inje��o de depend�ncia entre interface de reposit�rio e implementa��o
// A partir disso, � poss�vel acessar a implementa��o do reposit�rio a partir da interface
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITypeUserRepository, TypeUserRepository>();

// Services
builder.Services.AddScoped<IUserServices, UserServices>();

// Configura��o de Banco de Dados
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
