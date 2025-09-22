using System.Reflection;
using Microsoft.OpenApi.Models;

namespace Caixaverso.Backend.CrossCutting.Swagger
{
    public static class SwaggerConfiguration
    {
        public static void AddSwaggerDocumentation(this IServiceCollection services, IConfiguration configuration)
        {
            var swaggerSection = configuration.GetSection("Swagger");
            var contactSection = swaggerSection.GetSection("Contact");

            var contactInfo = new OpenApiContact
            {
                Name = contactSection["Name"],
                Email = contactSection["Email"],
                Url = new Uri(contactSection["Url"]!)
            };

            var versions = swaggerSection.GetSection("Versions").GetChildren();

            services.AddSwaggerGen(c =>
            {
                foreach (var version in versions)
                {
                    var name = version["Name"];
                    var title = version["Title"];
                    var description = version["Description"];

                    if (!string.IsNullOrEmpty(name))
                    {
                        c.SwaggerDoc(name, new OpenApiInfo
                        {
                            Title = title ?? "API",
                            Version = name,
                            Description = description ?? "Documentação da API",
                            Contact = contactInfo
                        });
                    }
                }

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath);
                }
            });
        }

        public static void UseSwaggerDocumentation(this IApplicationBuilder app, IConfiguration configuration)
        {
            var versions = configuration.GetSection("Swagger:Versions").GetChildren();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                foreach (var version in versions)
                {
                    var name = version["Name"];
                    var title = version["Title"];

                    if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(title))
                    {
                        c.SwaggerEndpoint($"/swagger/{name}/swagger.json", title);
                    }
                }

                c.RoutePrefix = string.Empty;
            });
        }

        private static Uri TryCreateUri(string? uriString, string fallback)
        {
            if (Uri.TryCreate(uriString, UriKind.Absolute, out var result))
            {
                return result;
            }

            return new Uri(fallback);
        }
    }
}
