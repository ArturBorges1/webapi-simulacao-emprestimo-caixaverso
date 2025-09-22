namespace Caixaverso.Backend.Domain.Entities
{
    /// <summary>
    /// Representa uma parcela da simulação de empréstimo.
    /// </summary>
    public class Parcela
    {
        /// <summary>
        /// Número do mês da parcela.
        /// </summary>
        public int Mes { get; set; }

        /// <summary>
        /// Saldo devedor no início do mês.
        /// </summary>
        public decimal SaldoDevedorInicial { get; set; }

        /// <summary>
        /// Valor dos juros aplicados no mês.
        /// </summary>
        public decimal Juros { get; set; }

        /// <summary>
        /// Valor da amortização (parte da parcela que reduz o saldo devedor).
        /// </summary>
        public decimal Amortizacao { get; set; }

        /// <summary>
        /// Valor total da parcela no mês (juros + amortização).
        /// </summary>
        public decimal ValorParcela { get; set; }

        /// <summary>
        /// Saldo devedor ao final do mês.
        /// </summary>
        public decimal SaldoDevedorFinal { get; set; }
    }
}
