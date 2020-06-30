using System;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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
            desc.GroupName, new Microsoft.OpenApi.Models.OpenApiInfo()
            {
              Title = $"Parky API {desc.ApiVersion}",
              Version = desc.ApiVersion.ToString()
            }
          );
      }
    }
  }
}
