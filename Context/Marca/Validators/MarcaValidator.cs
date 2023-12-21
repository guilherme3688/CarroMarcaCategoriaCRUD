namespace ProjetoCarro.Context.Validators {
    public class MarcaValidator {
        private readonly CarroContext _db;

        public MarcaValidator(CarroContext db) {
            _db = db;
        }

        public bool ValidateExcluir(int idMarca) {
            if (!ValidarRelacionamentos(idMarca)) { return false; }

            return true;
        }

        private bool ValidarRelacionamentos(int idMarca) {
            var temCarrosVinculadosMarca = _db.Carros
                .Where(c => c.Id == idMarca && c.DataExclusao == null)
                .Any();

            if (temCarrosVinculadosMarca) {
                return false;
            }
            else {
                return true;
            }
        }
    }
}
