namespace Caixaverso.Backend.API.Requests
{
    /// <summary>
    /// Representa os dados necessários para realizar uma simulação de empréstimo.
    /// </summary>
    public class SimulacaoEmprestimoRequest
    {
        /// <summary>
        /// Identificador do produto financeiro que será utilizado na simulação.
        /// </summary>
        /// <example>1</example>
        public required int IdProduto { get; set; }

        /// <summary>
        /// Valor total solicitado para o empréstimo.
        /// </summary>
        /// <example>10000.00</example>
        public required decimal ValorSolicitado { get; set; }

        /// <summary>
        /// Prazo de pagamento do empréstimo em meses.
        /// </summary>
        /// <example>24</example>
        public required int PrazoMeses { get; set; }
    }
}
