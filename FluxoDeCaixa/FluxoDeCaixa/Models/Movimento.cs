using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FluxoDeCaixa.Models
{
    public class Movimento
    {
        [Required]
        [SwaggerSchema(ReadOnly = true)]
        public int MovimentoId { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Valor { get; set; }

        [Required]
        [SwaggerSchema(ReadOnly = true)]
        public bool Sucesso { get; set; }

        [Required]
        [StringLength(200)]
        public string Motivo { get; set; } = "";

        [Required]
        [StringLength(200)]
        public string Detalhe { get; set; } = "";

        [Required]
        [Display(Name = "Caixa")]
        public virtual int CaixaId { get; set; }

        [ForeignKey("CaixaId")]
        [SwaggerSchema(ReadOnly = true)]
        public virtual Caixa? Caixa { get; set; }

        [Required]
        public DateTime Cadastro { get; set; }
    }
}