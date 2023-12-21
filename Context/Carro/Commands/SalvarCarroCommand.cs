using ProjetoCarro.Models;

namespace ProjetoCarro.Context.Commands {
    public class SalvarCarroCommand {
        private readonly CarroContext _db;

        public SalvarCarroCommand(CarroContext db) {
            _db = db;
        }
        public Carro Execute(CarroViewModel carro) {
            Carro retorno;

            if (carro.Id.HasValue) {
                retorno = Editar(carro);
            }
            else {
                retorno = Inserir(carro);
            }

            return retorno;
        }

        private Carro Inserir(CarroViewModel carro) {
            var carroModelo = new Carro {
                Modelo = carro.Modelo,
                AnoLancamento = carro.AnoLancamento,
                DataCadastro = DateTime.Now,
                IdCategoria = carro.IdCategoria,
                IdMarca = carro.IdMarca
            };

            _db.Carros.Add(carroModelo);

            return carroModelo;
        }

        private Carro Editar(CarroViewModel carro) {
            var carroExistente = _db.Carros
                .Where(c => c.Id == carro.Id && c.DataExclusao == null)
                .First();

            carroExistente.AnoLancamento = carro.AnoLancamento;
            carroExistente.Modelo = carro.Modelo;
            carroExistente.IdCategoria = carro.IdCategoria;
            carroExistente.IdMarca = carro.IdMarca;

            _db.Update(carroExistente);

            return carroExistente;
        }
    }
}
