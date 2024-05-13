using System.Globalization;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Rent_Premises.DAL.Context;
using Rent_Premises.DAL.Entities.Base;
using Rent_Premises.DAL.Repository;
using Rent_Premises.DAL.Repository.Interfaces;
using Rent_Premises.Extensions;
using RentPremises.BLL.Services.Auth;
using RentPremises.BLL.Services.Auth.Interfaces;
using RentPremises.BLL.Services.Premises;
using RentPremises.BLL.Services.Premises.Interfaces;
using RentPremises.BLL.Services.Rent;
using RentPremises.BLL.Services.Rent.Interfaces;
using RentPremises.BLL.Services.TypeOfPremises;
using RentPremises.BLL.Services.TypeOfPremises.Interfaces;
using RentPremises.Common.Models.Configs;
using RentPremises.Mapping.Profiles;

var builder = WebApplication.CreateBuilder(args);
//Configs
var jwtConfig = new JwtConfig();
builder.Configuration.Bind("Jwt", jwtConfig);
builder.Services.AddSingleton(jwtConfig);

//Services
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        .UseLoggerFactory(LoggerFactory.Create(cfg => cfg.AddConsole())));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IAuth, AuthService>();
builder.Services.AddScoped<IPremises, PremisesService>();
builder.Services.AddScoped<ITypeOfPremises, TypeOfPremisesService>();
builder.Services.AddScoped<IRent, RentService>();
builder.Services.AddAutoMapper(typeof(AuthProfile));
builder.Services.AddAutoMapper(typeof(PremisesProfile));
builder.Services.AddAutoMapper(typeof(TypeOfPremisesProfile));
builder.Services.AddAutoMapper(typeof(RentProfile));


//Auth
builder.Services.AddIdentity<User, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddTokenProvider<DataProtectorTokenProvider<User>>(TokenOptions.DefaultProvider);

var tokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuerSigningKey = true,
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Secret)),
    ValidIssuer = jwtConfig.Issuer,
    ValidAudience = jwtConfig.Audience,
    ClockSkew = jwtConfig.ClockSkew
};
builder.Services.AddSingleton(tokenValidationParameters);

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = tokenValidationParameters;
    });

//Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Storage API", Version = "v1" });

    c.AddSecurityDefinition("Bearer",
        new OpenApiSecurityScheme
        {
            Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

await app.SetupRolesAsync();

app.Run();