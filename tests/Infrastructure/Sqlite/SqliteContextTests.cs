using Microsoft.EntityFrameworkCore;
using Caixaverso.Backend.Domain.Entities;
using Caixaverso.Backend.Infrastructure.Sqlite.Context;

namespace Caixaverso.Backend.Tests.Infrastructure.Context
{
    public class SqliteContextTests
    {
        [Fact(DisplayName = "Construtor padrão deve instanciar o contexto")]
        public void ConstrutorPadrao_DeveInstanciarContexto()
        {
            // Act
            var contexto = new SqliteContext();

            // Assert
            Assert.NotNull(contexto);
        }

        [Fact(DisplayName = "Construtor com opções deve instanciar o contexto")]
        public void ConstrutorComOpcoes_DeveInstanciarContexto()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<SqliteContext>()
                .UseInMemoryDatabase("DbTesteConstrutor")
                .Options;

            // Act
            var contexto = new SqliteContext(options);

            // Assert
            Assert.NotNull(contexto);
        }

        [Fact(DisplayName = "Propriedade Produtos deve permitir acesso ao DbSet")]
        public void Produtos_DeveSerAcessivel()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<SqliteContext>()
                .UseInMemoryDatabase("DbTesteProdutos")
                .Options;

            var contexto = new SqliteContext(options);

            // Act
            var produtos = contexto.Produtos;

            // Assert
            Assert.NotNull(produtos);
            Assert.IsAssignableFrom<DbSet<Produto>>(produtos);
        }
    }
}
