namespace ProjetoCarro.Context.Commands {
    public class ExcluirCarroCommand {
        private readonly CarroContext _db;

        public ExcluirCarroCommand(CarroContext db) {
            _db = db;
        }

        public bool Execute(int id) {
            var carroExistente = _db.Carros
                .Where(c => c.Id == id)
                .First();

            carroExistente.DataExclusao = DateTime.Now;

            _db.Update(carroExistente);

            return true;
        }
    }
}
