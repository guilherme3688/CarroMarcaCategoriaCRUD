using ProjetoCarro.Models;

namespace ProjetoCarro.Context.Commands {
    public class SalvarCategoriaCommand {
        private readonly CarroContext _db;

        public SalvarCategoriaCommand(CarroContext db) {
            _db = db;
        }

        public Categoria Execute(CategoriaViewModel categoria){
            Categoria retorno;

            if (categoria.Id.HasValue) {
                retorno = Editar(categoria);
            } else {
                retorno = Inserir(categoria);
            }

            return retorno;
        }

        private Categoria Inserir(CategoriaViewModel categoria) {
            var categoriaModelo = new Categoria {
                NomeCategoria = categoria.NomeCategoria,
                DataCadastro = DateTime.Now
            };

            _db.Categorias.Add(categoriaModelo);

            return categoriaModelo;
        }

        private Categoria Editar(CategoriaViewModel categoria) {
            var categoriaExistente = _db.Categorias
                .Where(c => c.Id == categoria.Id && c.DataExclusao == null)
                .FirstOrDefault();

            if (categoriaExistente == null) { return null;  }

            categoriaExistente.NomeCategoria = categoria.NomeCategoria;

            _db.Update(categoriaExistente);

            return categoriaExistente;
        }
    }
}
