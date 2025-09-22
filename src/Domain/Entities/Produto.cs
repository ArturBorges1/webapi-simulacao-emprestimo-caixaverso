using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Caixaverso.Backend.Domain.Entities
{
    /// <summary>
    /// Representa a entidade de produto de empréstimo no banco de dados.
    /// </summary>
    [Table("PRODUTOS")]
    public class Produto
    {
        /// <summary>
        /// Identificador único do produto.
        /// </summary>
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        /// <summary>
        /// Nome do produto.
        /// </summary>
        [Column("NOME")]
        public required string Nome { get; set; }

        /// <summary>
        /// Taxa de juros anual aplicada ao produto.
        /// </summary>
        [Column("TAXA_JUROS_ANUAL")]
        public required double TaxaJurosAnual { get; set; }

        /// <summary>
        /// Prazo máximo em meses para pagamento.
        /// </summary>
        [Column("PRAZO_MAXIMO_MESES")]
        public required int PrazoMaximoMeses { get; set; }
    }
}
