using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoCarro.Context {
    [Table("Marcas")]
    public class Marca : EntidadeBase {
        [Required]
        [StringLength(50)]
        public string NomeMarca { get; set; }
    }
}
