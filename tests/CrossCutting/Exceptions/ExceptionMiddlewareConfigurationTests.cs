using Moq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Caixaverso.Backend.CrossCutting.Exceptions;

namespace Caixaverso.Backend.Tests.CrossCutting.Exceptions
{
    public class ExceptionMiddlewareConfigurationTests
    {
        [Fact(DisplayName = "Deve adicionar o middleware de exceção ao pipeline")]
        public void UseGlobalExceptionHandling_DeveAdicionarMiddlewareAoPipeline()
        {
            // Arrange
            var mockAppBuilder = new Mock<IApplicationBuilder>();

            // Simula encadeamento de chamadas
            mockAppBuilder
                .Setup(x => x.Use(It.IsAny<Func<RequestDelegate, RequestDelegate>>()))
                .Returns(mockAppBuilder.Object);

            // Act
            ExceptionMiddlewareConfiguration.UseGlobalExceptionHandling(mockAppBuilder.Object);

            // Assert
            mockAppBuilder.Verify(
                x => x.Use(It.IsAny<Func<RequestDelegate, RequestDelegate>>()),
                Times.Once,
                "O middleware de exceção deve ser registrado uma vez no pipeline."
            );
        }
    }
}
