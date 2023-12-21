using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoCarro.Context {
    [Table("Categorias")]
    public class Categoria : EntidadeBase {
        [Required]
        [StringLength(50)]
        public string NomeCategoria { get; set; }
    }
}
