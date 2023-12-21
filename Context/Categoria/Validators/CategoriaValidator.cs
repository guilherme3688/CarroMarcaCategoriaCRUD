namespace ProjetoCarro.Context.Validators {
    public class CategoriaValidator {
        private readonly CarroContext _db;

        public CategoriaValidator(CarroContext db) {
            _db = db;
        }

        public bool ValidateExcluir(int idCategoria) {
            if (!ValidarRelacionamentos(idCategoria)) { return false; }

            return true;
        }

        private bool ValidarRelacionamentos(int idCategoria) {
            var temCarrosVinculadosCategoria = _db.Carros
                .Where(c => c.IdCategoria == idCategoria && c.DataExclusao == null)
                .Any();

            if (temCarrosVinculadosCategoria) {
                return false;
            }
            else {
                return true;
            }
        }
    }
}
