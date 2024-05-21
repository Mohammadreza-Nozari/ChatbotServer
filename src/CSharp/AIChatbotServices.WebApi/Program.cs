using AIChatbotServices.Database.Contexts;
using AIChatbotServices.Logics;
using AIChatbotServices.Mappers;
using AICore.WebApi;
using AIServices.GeneratedServices;
using AutoMapper;
using EasyMicroservices.Database.EntityFrameworkCore.Providers;
using EasyMicroservices.Database.Interfaces;
using EasyMicroservices.Mapper.AutoMapper.Providers;
using EasyMicroservices.Mapper.Interfaces;
using EasyMicroservices.Security.Interfaces;
using EasyMicroservices.Security.Providers.HashProviders;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace AIChatbotServices.WebApi;

public class Program
{
    public static async Task Main(string[] args)
    {
        await ApplicationExecutor.RunApplication(args, (builder) =>
        {
            var configuration = builder.Configuration;
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["Jwt:Issuer"],
                        ValidAudience = configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                    };
                });
            builder.Services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    In = ParameterLocation.Header,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                config.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });

            builder.Services.AddScoped(services =>
            {
                var config = services.GetRequiredService<IConfiguration>();
                return new BotClient(config.GetSection("AIServiceAddress").Value, new HttpClient());
            });

            builder.Services.AddScoped<AuthenticationLogic>();
            builder.Services.AddScoped<IHashProvider>(x => new SHA1HashProvider());
            builder.Services.AddScoped<IMapperProvider>(x => new AutoMapperProvider(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            })));
            builder.Services.AddScoped<IDatabase>(services =>
            {
                var config = services.GetRequiredService<IConfiguration>();
                return new EntityFrameworkCoreDatabaseProvider(new ChatBotContext(config));
            });
        }, async app =>
        {
            app.UseCors(options =>
            {
                options.SetIsOriginAllowed(origin => true)
                .AllowAnyHeader()
                .AllowAnyMethod();
            });
            app.UseAuthentication();

            using (var scope = app.Services.CreateScope())
            {
                var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
                using var context = new ChatBotContext(config);
                await context.Database.MigrateAsync();
            }
        });
    }
}
