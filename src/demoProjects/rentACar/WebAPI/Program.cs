using Persistence;
using Application;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Encryption;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Core.Security.JWT;
using Microsoft.OpenApi.Models;

//بسم الله الرحمن الرحيم
/**
 *
 * @author Huseyin_Aydin
 * @since 1994
 * @category DotNet Core nArchitechture, C#.
 *
 */

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationServices();
builder.Services.AddSecurityServices();
builder.Services.AddPersistenceServices(builder.Configuration);
//builder.Services.AddInfrastructureServices();
builder.Services.AddHttpContextAccessor();

TokenOptions? tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = tokenOptions.Issuer,
        ValidAudience = tokenOptions.Audience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
    };
});

builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description =
            "JWT kimlik doğrulama ve yetkilendirmesi gerekiyor. \r\n\r\n Lütfen 'Bearer' kısmını giriniz [space] ve JWT token(jeton/hash)'i input'a giriniz..\r\n\r\nÖrnek bir JWT token(jeton/hash): \"Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkjDvHNleWluIEFZRElOIiwiYWRtaW4iOnRydWUsImlhdCI6MTUxNjIzOTAyMn0.MIEx3cnmbDS_I47HV8VO5rHmqynFudMK0caWy9dq6623EyVvhCWfdM-0Vzr9QuxALE3u-lGkezVHfsVqo-uwXQ\""
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
                { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } },
            new string[] { }
        }
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//if (app.Environment.IsProduction())
//app.ConfigureCustomExceptionMiddleware();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();