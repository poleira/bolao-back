using BolaoDaCopa.Aplicacao.Boloes.Profiles;
using BolaoDaCopa.Aplicacao.Boloes.Servicos;
using BolaoDaCopa.Aplicacao.Boloes.Servicos.Interfaces;
using BolaoDaCopa.Aplicacao.HabilitarPalpites.Servicos;
using BolaoDaCopa.Aplicacao.Palpites.Servicos.Interfaces;
using BolaoDaCopa.Aplicacao.Rank.Servicos;
using BolaoDaCopa.Aplicacao.Rank.Servicos.Interfaces;
using BolaoDaCopa.Infra.Mapeamento;
using BolaoDaCopa.Infra.Repositorios.Boloes;
using BolaoDaCopa.Infra.Repositorios.Boloes.Interfaces;
using BolaoDaCopa.Infra.Repositorios.Usuarios;
using BolaoDaCopa.Infra.Repositorios.Usuarios.Interfaces;
using BolaoTeste;
using BolaoTeste.Aplicacao.HabilitarPalpites.Servicos.Interfaces;
using BolaoTeste.Aplicacao.Palpites.Servicos;
using BolaoTeste.Data.Repositorios;
using BolaoTeste.Data.Repositorios.Interfaces;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NHibernate;
using System.Text;
using ISession = NHibernate.ISession;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAuthentication(opt => {
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "https://localhost:7288",
            ValidAudience = "https://localhost:7288",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("bsdhfgskdj32njkndwj4odhdol3n2dk")),
            
    };
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(BoloesProfile));


builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsImplementationPolicy", builder => { builder.WithOrigins("*");
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


builder.Services.AddSingleton<ISession>(factory => factory.GetService<ISessionFactory>()!.OpenSession());

builder.Services.AddSingleton<IBoloesRepositorio, BoloesRepositorio>();
builder.Services.AddSingleton<IPalpiteRepositorio, PalpiteRepositorio>();
builder.Services.AddSingleton<IUsuariosRepositorio, UsuariosRepositorio>();
builder.Services.AddSingleton<IHabilitarPalpiteRepositorio, HabilitarPalpiteRepositorio>();
builder.Services.AddSingleton<IBoloesServico, BoloesServico>();
builder.Services.AddSingleton<IHabilitarPalpiteServico, HabilitarPalpiteServico>();
builder.Services.AddSingleton<IRankServico, RankServico>();
builder.Services.AddSingleton<IPalpiteServico, PalpiteServico>();



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
