using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Caixaverso.Backend.CrossCutting.Versioning;

namespace Caixaverso.Backend.Tests.CrossCutting.Versioning
{
    public class ApiVersioningConfigTests
    {
        [Fact]
        public void AddApiVersioningConfiguration_ShouldRegisterVersioningServices()
        {
            // Arrange
            var services = new ServiceCollection();

            // Adiciona suporte a logging para evitar erro de ILogger<T>
            services.AddLogging();

            // Act
            services.AddApiVersioningConfiguration();
            var provider = services.BuildServiceProvider();

            // Assert
            var versioning = provider.GetService<IApiVersionDescriptionProvider>();
            Assert.NotNull(versioning);
        }
    }
}
