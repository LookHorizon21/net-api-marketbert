using ApiMarketBert.Data;
using ApiMarketBert.Repository.IRepository;
using ApiMarketBert.Repository;
using Microsoft.EntityFrameworkCore;
using ApiMarketBert.MarketMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
                opciones.UseSqlServer(builder.Configuration.GetConnectionString("ConexionSql")));

//Agregamos los Repositorios
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

var key = builder.Configuration.GetValue<string>("ApiSettings:secret");

//Agregamos el AutoMapper
builder.Services.AddAutoMapper(typeof(MarketMapper));

//Autenciación
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Bearer
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description =
        "Autenticación JWT usando el esquema Bearer. \r\n\r\n " +
        "Para autentificarse, ingrese la palabra 'Bearer' + [espacio] + [Token] en el campo de Value: \r\n\r\n" +
        "Ejemplo: \"Bearer tkdknkdllskd\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

//Soporte para CORS: http://localhost:3223
builder.Services.AddCors(p => p.AddPolicy("PoliticaCors", build =>
{
    build.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || true) // Forzar Swagger en producción
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Soporte para CORS
app.UseCors("PolicyCors");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
