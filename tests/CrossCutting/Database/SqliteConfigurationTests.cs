using Xunit;
using Moq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Caixaverso.Backend.CrossCutting.Database;
using Caixaverso.Backend.Infrastructure.Sqlite.Context;

namespace Caixaverso.Backend.Tests.CrossCutting.Database
{
    public class SqliteConfigurationTests
    {
        [Fact(DisplayName = "Deve registrar SqliteContext no container de serviços usando IConfiguration mockado")]
        public void AddSqlite_DeveRegistrarDbContext_ComConfiguracaoMockada()
        {
            // Arrange
            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration
                .Setup(x => x["ConnectionStrings:Database"])
                .Returns("caixaverso.db");

            var services = new ServiceCollection();

            // Act
            services.AddSqlite(mockConfiguration.Object);
            var provider = services.BuildServiceProvider();

            // Assert
            var context = provider.GetService<SqliteContext>();
            Assert.NotNull(context);
            Assert.IsType<SqliteContext>(context);
        }

        [Fact(DisplayName = "Deve construir corretamente a string de conexão SQLite usando IConfiguration mockado")]
        public void AddSqlite_DeveConstruirStringDeConexaoCorreta_ComConfiguracaoMockada()
        {
            // Arrange
            var nomeBancoEsperado = "caixaverso.db";

            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration
                .Setup(x => x["ConnectionStrings:Database"])
                .Returns(nomeBancoEsperado);

            var services = new ServiceCollection();

            // Act
            services.AddSqlite(mockConfiguration.Object);
            var provider = services.BuildServiceProvider();
            var context = provider.GetRequiredService<SqliteContext>();

            // Assert
            var connectionString = context.Database.GetDbConnection().ConnectionString;
            Assert.Contains(nomeBancoEsperado, connectionString);
        }

        [Fact(DisplayName = "Deve retornar a mesma instância de IServiceCollection após configuração")]
        public void AddSqlite_DeveRetornarMesmoServiceCollection()
        {
            // Arrange
            var mockConfiguration = new Mock<IConfiguration>();
            mockConfiguration
                .Setup(x => x["ConnectionStrings:Database"])
                .Returns("caixaverso.db");

            var services = new ServiceCollection();

            // Act
            var resultado = services.AddSqlite(mockConfiguration.Object);

            // Assert
            Assert.Same(services, resultado);
        }
    }
}
