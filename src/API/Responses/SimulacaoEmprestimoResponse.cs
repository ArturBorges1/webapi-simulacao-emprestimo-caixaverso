using Caixaverso.Backend.Domain.Entities;

namespace Caixaverso.Backend.API.Responses
{
    /// <summary>
    /// Representa a resposta de uma simulação de empréstimo.
    /// </summary>
    public class SimulacaoEmprestimoResponse
    {
        /// <summary>
        /// Dados do produto utilizado na simulação.
        /// </summary>
        public required Produto Produto { get; set; }

        /// <summary>
        /// Valor solicitado pelo cliente.
        /// </summary>
        public required decimal ValorSolicitado { get; set; }

        /// <summary>
        /// Prazo de pagamento em meses.
        /// </summary>
        public required int PrazoMeses { get; set; }

        /// <summary>
        /// Taxa de juros efetiva mensal calculada a partir da taxa anual.
        /// </summary>
        public required double TaxaJurosEfetivaMensal { get; set; }

        /// <summary>
        /// Valor total a ser pago com juros ao final do prazo.
        /// </summary>
        public required decimal ValorTotalComJuros { get; set; }

        /// <summary>
        /// Valor fixo da parcela mensal.
        /// </summary>
        public required decimal ParcelaMensal { get; set; }

        /// <summary>
        /// Memória de cálculo mês a mês, contendo saldo devedor, juros e amortização.
        /// </summary>
        public required List<Parcela> MemoriaCalculo { get; set; }

        /// <summary>
        /// Cria uma resposta de simulação com valores zerados.
        /// </summary>
        /// <param name="produto">Produto utilizado na simulação.</param>
        /// <param name="valorSolicitado">Valor solicitado pelo cliente.</param>
        /// <param name="prazoMeses">Prazo de pagamento em meses.</param>
        /// <returns>Objeto de resposta com campos inicializados.</returns>

        public static SimulacaoEmprestimoResponse Calcular(Produto produto, decimal valorSolicitado, int prazoMeses)
        {
            var taxaEfetivaMensal = Math.Pow(1 + (double)produto.TaxaJurosAnual / 100, 1.0 / 12) - 1;
            var parcela = valorSolicitado * (decimal)((taxaEfetivaMensal * Math.Pow(1 + taxaEfetivaMensal, prazoMeses)) /
                                                      (Math.Pow(1 + taxaEfetivaMensal, prazoMeses) - 1));
            var memoria = new List<Parcela>();
            var saldoDevedor = valorSolicitado;

            for (int mes = 1; mes <= prazoMeses; mes++)
            {
                var juros = saldoDevedor * (decimal)taxaEfetivaMensal;
                var amortizacao = parcela - juros;
                var saldoFinal = saldoDevedor - amortizacao;

                memoria.Add(new Parcela
                {
                    Mes = mes,
                    SaldoDevedorInicial = Math.Round(saldoDevedor, 2),
                    Juros = Math.Round(juros, 2),
                    Amortizacao = Math.Round(amortizacao, 2),
                    ValorParcela = Math.Round(parcela, 2),
                    SaldoDevedorFinal = Math.Round(saldoFinal, 2)
                });

                saldoDevedor = saldoFinal;
            }

            return new SimulacaoEmprestimoResponse
            {
                Produto = produto,
                ValorSolicitado = valorSolicitado,
                PrazoMeses = prazoMeses,
                TaxaJurosEfetivaMensal = Math.Round(taxaEfetivaMensal, 6),
                ValorTotalComJuros = Math.Round(parcela * prazoMeses, 2),
                ParcelaMensal = Math.Round(parcela, 2),
                MemoriaCalculo = memoria
            };
        }

    }
}
