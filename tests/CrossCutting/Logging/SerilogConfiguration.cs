using Serilog;
using Serilog.Events;
using Caixaverso.Backend.CrossCutting.Logging;

namespace Caixaverso.Backend.Tests.CrossCutting.Logging
{
    public class SerilogConfigurationTests
    {
        [Fact(DisplayName = "Deve configurar o logger global com nível mínimo Debug")]
        public void ConfigureSerilog_DeveDefinirNivelMinimoDebug()
        {
            // Act
            SerilogConfiguration.ConfigureSerilog();

            // Assert
            Assert.True(Log.Logger.IsEnabled(LogEventLevel.Debug), "O logger deve estar habilitado para nível Debug.");
        }

        [Fact(DisplayName = "Deve permitir registrar eventos de log após configuração")]
        public void ConfigureSerilog_DevePermitirRegistrarEventos()
        {
            // Act
            SerilogConfiguration.ConfigureSerilog();

            // Assert
            Assert.True(Log.Logger.IsEnabled(LogEventLevel.Information), "O logger deve estar habilitado para nível Information.");
            Assert.True(Log.Logger.IsEnabled(LogEventLevel.Warning), "O logger deve estar habilitado para nível Warning.");
        }

        [Fact(DisplayName = "Deve permitir escrita de log sem lançar exceções")]
        public void ConfigureSerilog_DevePermitirEscritaDeLog()
        {
            // Act
            SerilogConfiguration.ConfigureSerilog();

            // Assert
            var exception = Record.Exception(() =>
            {
                Log.Information("Teste de log funcional");
            });

            Assert.Null(exception);
        }
    }
}
