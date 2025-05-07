using BolaoDaCopa.Aplicacao.Boloes.Profiles;
using BolaoDaCopa.Aplicacao.Boloes.Servicos;
using BolaoDaCopa.Aplicacao.Boloes.Servicos.Interfaces;
using BolaoDaCopa.Aplicacao.BoloesUsuarios.Servicos;
using BolaoDaCopa.Aplicacao.BoloesUsuarios.Servicos.Interfaces;
using BolaoDaCopa.Aplicacao.Comum.Repositorios;
using BolaoDaCopa.Aplicacao.HabilitarPalpites.Servicos;
using BolaoDaCopa.Aplicacao.Jogadores.Servicos;
using BolaoDaCopa.Aplicacao.Jogadores.Servicos.Interfaces;
using BolaoDaCopa.Aplicacao.Palpites.Servicos;
using BolaoDaCopa.Aplicacao.Palpites.Servicos.Interfaces;
using BolaoDaCopa.Aplicacao.Rank.Servicos;
using BolaoDaCopa.Aplicacao.Rank.Servicos.Interfaces;
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
using BolaoDaCopa.Infra.Repositorios.NovaPasta.Interfaces;
using BolaoDaCopa.Infra.Repositorios.Palpites;
using BolaoDaCopa.Infra.Repositorios.Palpites.Interface;
using BolaoDaCopa.Infra.Repositorios.Selecoes;
using BolaoDaCopa.Infra.Repositorios.Selecoes.Interfaces;
using BolaoDaCopa.Infra.Repositorios.Usuarios;
using BolaoDaCopa.Infra.Repositorios.Usuarios.Interfaces;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using ISession = NHibernate.ISession;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddInfraestrutura(builder.Configuration);
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();
app.UseCors("MyCorsImplementationPolicy");
app.UseAuthentication();
app.UseAuthorization();



app.MapControllers();

app.Run();
