using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjetoCarro.Context {
    public class EntidadeBase {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

        [Display(Name = "Data de cadastro")]
        public DateTime DataCadastro { get; set; }

        [Display(Name = "Data de exclusão")]
        public DateTime? DataExclusao { get; set; }
    }
}
