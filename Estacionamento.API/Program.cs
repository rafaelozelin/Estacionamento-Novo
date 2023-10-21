using AutoMapper;
using Estacionamento.API.Abstract.Factories;
using Estacionamento.API.Abstract.Repositories;
using Estacionamento.API.Abstract.Services;
using Estacionamento.API.Application.Factory;
using Estacionamento.API.Application.Mapping;
using Estacionamento.API.Application.Services;
using Estacionamento.API.Infra.Contexts;
using Estacionamento.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IVeiculoRepository, VeiculoRepository>();
builder.Services.AddScoped<IVagaRepository, VagaRepository>();

builder.Services.AddScoped<IVeiculoService, CarroService>();
builder.Services.AddScoped<IVeiculoService, VanService>();
builder.Services.AddScoped<IVeiculoService, MotoService>();
builder.Services.AddScoped<IVagaService, VagaService>();

builder.Services.AddSingleton<IVeiculoServiceFactory, VeiculoServiceFactory>();

//builder.Services.AddSingleton<Func<IEnumerable<IVeiculoService>>>(x => () => x.GetService<IEnumerable<IVeiculoService>>()!);

builder.Services.AddSingleton<Func<IEnumerable<IVeiculoService>>>(
    serviceProvider => () => serviceProvider
                            .CreateScope()
                            .ServiceProvider
                            .GetService<IEnumerable<IVeiculoService>>()!);

var mapperConfig = MapperConfig.GetMapperConfig();
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

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
