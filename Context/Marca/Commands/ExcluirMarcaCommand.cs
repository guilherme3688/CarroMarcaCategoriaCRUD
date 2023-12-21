using ProjetoCarro.Context.Validators;

namespace ProjetoCarro.Context.Commands {
    public class ExcluirMarcaCommand {
        private readonly CarroContext _db;
        private readonly MarcaValidator validator;

        public ExcluirMarcaCommand(CarroContext db) {
            _db = db;
            validator = new MarcaValidator(db);
        }

        public bool Execute(int id) {
            if (!validator.ValidateExcluir(id)) { return false; }

            var marcaExistente = _db.Marcas
                .Where(c => c.Id == id)
                .First();

            marcaExistente.DataExclusao = DateTime.Now;

            _db.Update(marcaExistente);

            return true;
        }
    }
}
