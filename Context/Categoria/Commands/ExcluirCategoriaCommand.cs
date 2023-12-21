using ProjetoCarro.Context.Validators;

namespace ProjetoCarro.Context.Commands {
    public class ExcluirCategoriaCommand {
        private readonly CarroContext _db;
        private readonly CategoriaValidator validator;

        public ExcluirCategoriaCommand(CarroContext db) {
            _db = db;
            validator = new CategoriaValidator(db);
        }

        public bool Execute(int id) {
            if (!validator.ValidateExcluir(id)) { return false;  }

            var categoriaExistente = _db.Categorias
                .Where(c => c.Id == id)
                .First();

            categoriaExistente.DataExclusao = DateTime.Now;

            _db.Update(categoriaExistente);

            return true;
        }
    }
}
