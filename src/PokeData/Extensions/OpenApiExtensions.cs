﻿using Microsoft.OpenApi.Models;
using PokeData.Constants;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PokeData.Extensions;

internal static class OpenApiExtensions
{
  private const string Title = "PokéData API";
  private static readonly Version Version = new(0, 1, 0);

  public static IServiceCollection AddOpenApi(this IServiceCollection services)
  {
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(config =>
    {
      config.AddSecurity();
      config.SwaggerDoc(name: $"v{Version.Major}", new OpenApiInfo
      {
        Contact = new OpenApiContact
        {
          Email = "francispion@hotmail.com",
          Name = "Logitar Team",
          Url = new Uri("https://github.com/Utar94/PokeData", UriKind.Absolute)
        },
        Description = "Pokémon data management.",
        License = new OpenApiLicense
        {
          Name = "Use under MIT",
          Url = new Uri("https://github.com/Utar94/PokeData/blob/main/LICENSE", UriKind.Absolute)
        },
        Title = Title,
        Version = $"v{Version}"
      });
    });

    return services;
  }

  public static void UseOpenApi(this IApplicationBuilder builder)
  {
    builder.UseSwagger();
    builder.UseSwaggerUI(config => config.SwaggerEndpoint(
      url: $"/swagger/v{Version.Major}/swagger.json",
      name: $"{Title} v{Version}"
    ));
  }

  private static void AddSecurity(this SwaggerGenOptions options)
  {
    options.AddSecurityDefinition(Schemes.Basic, new OpenApiSecurityScheme
    {
      Description = "Enter your credentials in the inputs below:",
      In = ParameterLocation.Header,
      Name = Headers.Authorization,
      Scheme = Schemes.Basic,
      Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
      {
        new OpenApiSecurityScheme
        {
          In = ParameterLocation.Header,
          Name = Headers.Authorization,
          Reference = new OpenApiReference
          {
            Id = Schemes.Basic,
            Type = ReferenceType.SecurityScheme
          },
          Scheme = Schemes.Basic,
          Type = SecuritySchemeType.Http
        },
        new List<string>()
      }
    });
  }
}
