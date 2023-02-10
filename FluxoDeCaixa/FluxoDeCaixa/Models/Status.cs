using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace FluxoDeCaixa.Models
{
    public class Status
    {
        [Required]
        [SwaggerSchema(ReadOnly = true)]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Nome { get; set; } = "";
    }
}