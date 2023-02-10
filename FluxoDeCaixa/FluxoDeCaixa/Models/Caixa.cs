using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FluxoDeCaixa.Models
{
    public class Caixa
    {
        [Required]
        [SwaggerSchema(ReadOnly = true)]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Saldo { get; set; }

        [Required]
        [Display(Name = "Status")]
        public virtual int StatusId { get; set; }

        [ForeignKey("StatusId")]
        [SwaggerSchema(ReadOnly = true)]
        public virtual Status? Status { get; set; }
    }
}