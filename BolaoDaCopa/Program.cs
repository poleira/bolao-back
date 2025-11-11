using BolaoDaCopa.Aplicacao.Boloes.Profiles;
using BolaoDaCopa.Aplicacao.Boloes.Servicos;
using BolaoDaCopa.Aplicacao.Boloes.Servicos.Interfaces;
using BolaoDaCopa.Aplicacao.BoloesUsuarios.Servicos;
using BolaoDaCopa.Aplicacao.BoloesUsuarios.Servicos.Interfaces;
using BolaoDaCopa.Aplicacao.Comum.Repositorios;
using BolaoDaCopa.Aplicacao.HabilitarPalpites.Servicos;
using BolaoDaCopa.Aplicacao.Jogadores.Servicos;
using BolaoDaCopa.Aplicacao.Jogadores.Servicos.Interfaces;
using BolaoDaCopa.Aplicacao.Notificacoes.Servicos;
using BolaoDaCopa.Aplicacao.Notificacoes.Servicos.Interfaces;
using BolaoDaCopa.Aplicacao.Palpites.Servicos;
using BolaoDaCopa.Aplicacao.Palpites.Servicos.Interfaces;
using BolaoDaCopa.Aplicacao.Rank.Servicos;
using BolaoDaCopa.Aplicacao.Rank.Servicos.Interfaces;
using BolaoDaCopa.Aplicacao.Regras.Servicos;
using BolaoDaCopa.Aplicacao.Regras.Servicos.Interfaces;
using BolaoDaCopa.Aplicacao.Selecoes.Servicos.Interfaces;
using BolaoDaCopa.Aplicacao.Usuarios.Servicos;
using BolaoDaCopa.Aplicacao.Usuarios.Servicos.Interfaces;
using BolaoDaCopa.Bibliotecas.Transacoes;
using BolaoDaCopa.Bibliotecas.Transacoes.Interfaces;
using BolaoDaCopa.Infra;
using BolaoDaCopa.Infra.Autenticacao;
using BolaoDaCopa.Infra.Mapeamento;
using BolaoDaCopa.Infra.Repositorios.Boloes;
using BolaoDaCopa.Infra.Repositorios.Boloes.Interfaces;
using BolaoDaCopa.Infra.Repositorios.BoloesUsuarios;
using BolaoDaCopa.Infra.Repositorios.BoloesUsuarios.Interfaces;
using BolaoDaCopa.Infra.Repositorios.Jogadores;
using BolaoDaCopa.Infra.Repositorios.Notificacoes;
using BolaoDaCopa.Infra.Repositorios.Notificacoes.Interfaces;
using BolaoDaCopa.Infra.Repositorios.NovaPasta.Interfaces;
using BolaoDaCopa.Infra.Repositorios.Palpites;
using BolaoDaCopa.Infra.Repositorios.Palpites.Interface;
using BolaoDaCopa.Infra.Repositorios.Regras;
using BolaoDaCopa.Infra.Repositorios.Regras.Interfaces;
using BolaoDaCopa.Infra.Repositorios.Selecoes;
using BolaoDaCopa.Infra.Repositorios.Selecoes.Interfaces;
using BolaoDaCopa.Infra.Repositorios.Usuarios;
using BolaoDaCopa.Infra.Repositorios.Usuarios.Interfaces;
using BolaoDaCopa.Infra.Repositorios.ModoJogoRegra;
using BolaoDaCopa.Infra.Repositorios.ModoJogoRegra.Interfaces;
using BolaoDaCopa.Infra.Repositorios.ModosJogos;
using BolaoDaCopa.Infra.Repositorios.ModosJogos.Interfaces;
using BolaoDaCopa.Aplicacao.ModoJogo.Servicos;
using BolaoDaCopa.Aplicacao.ModoJogo.Servicos.Interfaces;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.OpenApi.Models;
using NHibernate;
using ISession = NHibernate.ISession;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddInfraestrutura(builder.Configuration);
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(BoloesProfile));


builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsImplementationPolicy", builder =>
    {
        builder.WithOrigins("*");
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
    });
});

builder.Services.AddSingleton<ISessionFactory>(factory =>
{
    //azure = "Data Source=bolao-hexa.mysql.database.azure.com;Initial Catalog=teste;User ID=polin;Password=@Botafogo123;SslMode=Required;"
    string connectionString = "Data Source=localhost;Initial Catalog=bolao;User ID=root;Password=root;";
    return Fluently.Configure().Database(MySQLConfiguration.Standard
                .ConnectionString(connectionString)
                .FormatSql()
                .ShowSql())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UsuarioMap>())
                .BuildSessionFactory();
});


builder.Services.AddScoped<ISession>(factory => factory.GetService<ISessionFactory>()!.OpenSession());

builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<IBoloesRepositorio, BoloesRepositorio>();
builder.Services.AddScoped<ISelecoesRepositorio, SelecoesRepositorio>();
builder.Services.AddScoped<IBoloesUsuariosServico, BoloesUsuariosServico>();
builder.Services.AddScoped<IBoloesUsuariosRepositorio, BoloesUsuariosRepositorio>();
builder.Services.AddScoped<IBoloesServico, BoloesServico>();
builder.Services.AddScoped<ISelecoesServico, SelecoesServico>();
builder.Services.AddScoped<IRankServico, RankServico>();
builder.Services.AddScoped<IPalpitesServico, PalpitesServico>();
builder.Services.AddScoped<IJogadoresServico, JogadoresServico>();
builder.Services.AddScoped<IJogadoresRepositorio, JogadoresRepositorio>();
builder.Services.AddScoped<IPalpitesRepositorio, PalpitesRepositorio>();
builder.Services.AddScoped<IUsuariosRepositorio, UsuariosRepositorio>();
builder.Services.AddScoped<IUsuariosServico, UsuariosServico>();
builder.Services.AddScoped<INotificacoesRepositorio, NotificacoesRepositorio>();
builder.Services.AddScoped<INotificacoesServico, NotificacoesServico>();
builder.Services.AddScoped<IRegrasRepositorio, RegrasRepositorio>();
builder.Services.AddScoped<IRegrasServico, RegrasServico>();
builder.Services.AddScoped<IModoJogoRegraRepositorio, ModoJogoRegraRepositorio>();
builder.Services.AddScoped<IModosJogosRepositorios, ModosJogosRepositorios>();
builder.Services.AddScoped<IModosJogosServicos, ModosJogosServicos>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Bolao Api", Version = "v1" });

    // Adiciona a configura��o do token
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Informe o token no formato: Bearer {seu token JWT}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 400; // Ou 500, conforme o caso
        context.Response.ContentType = "application/json";
        var error = context.Features.Get<IExceptionHandlerFeature>();
        if (error != null)
        {
            await context.Response.WriteAsync(
                System.Text.Json.JsonSerializer.Serialize(new { erro = error.Error.Message })
            );
        }
    });
});

app.UseHttpsRedirection();
app.UseCors("MyCorsImplementationPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
