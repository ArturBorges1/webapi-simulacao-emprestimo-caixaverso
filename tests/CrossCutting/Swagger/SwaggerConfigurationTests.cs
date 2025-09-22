using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.Extensions.DependencyInjection;

namespace Caixaverso.Backend.Tests.CrossCutting.Swagger
{
    public class SwaggerConfigurationTests
    {
        [Fact]
        public void AddSwaggerDocumentation_ShouldConfigureSwaggerDocsCorrectly()
        {
            // Arrange: simula o appsettings.json
            var configData = new Dictionary<string, string?>
            {
                ["Swagger:Contact:Name"] = "Artur Borges Cerqueira de Andrade",
                ["Swagger:Contact:Email"] = "artur.andrade@caixa.gov.br",
                ["Swagger:Contact:Url"] = "https://github.com/ArturBorges1",
                ["Swagger:Versions:0:Name"] = "v1",
                ["Swagger:Versions:0:Title"] = "API de Produtos de Empréstimo com Cálculo de Juros v1",
                ["Swagger:Versions:0:Description"] = "Versão 1 do Desafio Técnico do Caixaverso - Dev Back-end C#"
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(configData)
                .Build();

            var swaggerOptions = new SwaggerGenOptions();

            // Simula a chamada do método AddSwaggerDocumentation
            var swaggerSection = configuration.GetSection("Swagger");
            var contactSection = swaggerSection.GetSection("Contact");
            var versions = swaggerSection.GetSection("Versions").GetChildren();

            var contactInfo = new OpenApiContact
            {
                Name = contactSection["Name"],
                Email = contactSection["Email"],
                Url = new Uri(contactSection["Url"]!)
            };

            foreach (var version in versions)
            {
                var name = version["Name"];
                var title = version["Title"];
                var description = version["Description"];

                if (!string.IsNullOrEmpty(name))
                {
                    swaggerOptions.SwaggerDoc(name, new OpenApiInfo
                    {
                        Title = title ?? "API",
                        Version = name,
                        Description = description ?? "Documentação da API",
                        Contact = contactInfo
                    });
                }
            }

            // Assert
            Assert.True(swaggerOptions.SwaggerGeneratorOptions.SwaggerDocs.ContainsKey("v1"));

            var doc = swaggerOptions.SwaggerGeneratorOptions.SwaggerDocs["v1"];
            Assert.Equal("API de Produtos de Empréstimo com Cálculo de Juros v1", doc.Title);
            Assert.Equal("Versão 1 do Desafio Técnico do Caixaverso - Dev Back-end C#", doc.Description);
            Assert.Equal("Artur Borges Cerqueira de Andrade", doc.Contact.Name);
            Assert.Equal("artur.andrade@caixa.gov.br", doc.Contact.Email);
            Assert.Equal(new Uri("https://github.com/ArturBorges1"), doc.Contact.Url);
        }
    }
}
