namespace ProjetoCarro.Models {
    public class CarroViewModel {
        public int? Id { get; set; }
        public string Modelo { get; set; }
        public string AnoLancamento { get; set; }
        public int IdMarca { get; set; }
        public int IdCategoria { get; set; }
        public DateTime? DataCadastro { get; set; }
    }
}
