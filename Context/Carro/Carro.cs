using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjetoCarro.Context {
    [Table("Carros")]
    public class Carro : EntidadeBase {
        [Required]
        public string Modelo { get; set; }

        [StringLength(4)]
        public string AnoLancamento { get; set; }

        [ForeignKey("Marca")]
        public int IdMarca { get; set; }

        [ForeignKey("Categoria")]
        public int IdCategoria { get; set; }

        public virtual Marca Marca { get; set; }

        public virtual Categoria Categoria { get; set; }
    }
}
