namespace Caixaverso.Backend.API.Requests
{
    /// <summary>
    /// Representa os dados necessários para criar um novo produto de empréstimo.
    /// </summary>
    public class CriarProdutoRequest
    {
        /// <summary>
        /// Nome do produto de empréstimo.
        /// </summary>
        /// <example>Empréstimo Pessoal</example>
        public required string Nome { get; set; }

        /// <summary>
        /// Taxa de juros anual aplicada ao produto.
        /// </summary>
        /// <example>12.5</example>
        public required double TaxaJurosAnual { get; set; }

        /// <summary>
        /// Prazo máximo em meses para pagamento do empréstimo.
        /// </summary>
        /// <example>36</example>
        public required int PrazoMaximoMeses { get; set; }
    }
}
