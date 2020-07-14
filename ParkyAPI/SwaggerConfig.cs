using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ParkyAPI
{
  public class SwaggerConfig : IConfigureOptions<SwaggerGenOptions>
  {
    readonly IApiVersionDescriptionProvider provider;

    public SwaggerConfig(IApiVersionDescriptionProvider provider) => this.provider = provider;

    public void Configure(SwaggerGenOptions options)
    {
      foreach(var desc in provider.ApiVersionDescriptions)
      {
        options.SwaggerDoc(
            desc.GroupName, new OpenApiInfo()
            {
              Title = $"Parky API {desc.ApiVersion}",
              Version = desc.ApiVersion.ToString()
            }
          );
      }

      options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
      {
        Description = "JWT authorisation header using the Bearer Scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below. \r\n\r\n Example: \"Bearer abc12345def\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
      });

      options.AddSecurityRequirement(new OpenApiSecurityRequirement()
      {
        {
          new OpenApiSecurityScheme()
          {
            Reference = new OpenApiReference
            {
              Type = ReferenceType.SecurityScheme,
              Id = "Bearer"
            },
            Scheme = "oauth2",
            Name= "Bearer",
            In = ParameterLocation.Header,
          },
          new List<string>()
        }
      });

      var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
      var cmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);
      options.IncludeXmlComments(cmlCommentsFullPath);
    }
  }
}
