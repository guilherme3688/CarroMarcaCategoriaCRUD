using ProjetoCarro.Models;

namespace ProjetoCarro.Context.Commands {
    public class SalvarMarcaCommand {
        private readonly CarroContext _db;

        public SalvarMarcaCommand(CarroContext db) {
            _db = db;
        }

        public Marca Execute(MarcaViewModel marca) {
            Marca retorno;
           
            if (marca.Id.HasValue) {
                retorno = Editar(marca);
            } else {
                retorno = Inserir(marca);
            }

            return retorno;
        }

        private Marca Inserir(MarcaViewModel marca) {
            var marcaModelo = new Marca {
                NomeMarca = marca.NomeMarca,
                DataCadastro = DateTime.Now
            };

            _db.Marcas.Add(marcaModelo);

            return marcaModelo;
        }

        private Marca Editar(MarcaViewModel marca) {
            var marcaExistente = _db.Marcas
                .Where(c => c.Id == marca.Id && c.DataExclusao == null)
                .First();

            marcaExistente.NomeMarca = marca.NomeMarca;

            _db.Update(marcaExistente);

            return marcaExistente;
        }

    }
}
