using Microsoft.AspNetCore.Mvc;
using ProjetoCarro.Context;
using ProjetoCarro.Context.Commands;
using ProjetoCarro.Models;

namespace ProjetoCarro.Controllers {
    public class CarroController : Controller {
        private readonly CarroContext _db;
        private readonly SalvarCarroCommand salvarCommand;
        private readonly ExcluirCarroCommand excluirCommand;

        public CarroController(CarroContext db) {
            _db = db;
            salvarCommand = new SalvarCarroCommand(db);
            excluirCommand = new ExcluirCarroCommand(db);
        }

        public ActionResult Index() {
            return View();
        }

        [HttpPost]
        public virtual ActionResult Salvar([FromBody] CarroViewModel carro) {
            if (!ModelState.IsValid) { return null; }

            var retorno = salvarCommand.Execute(carro);

            if (retorno == null) { return Json(new { Erro = "Não foi possivel salvar o veículo! Revise as informações e tente novamente" }); }

            _db.SaveChanges();

            return Json(new { Id = retorno.Id, Mensagem = "Veículo salvo com sucesso!" });
        }

        public virtual ActionResult Editar(int id){
            var retorno = _db.Carros
                .Where(c => c.Id == id && c.DataExclusao == null)
                .Select(c => new CarroViewModel {
                    Id = c.Id,
                    Modelo = c.Modelo,
                    AnoLancamento = c.AnoLancamento,
                    IdMarca = c.IdMarca,
                    IdCategoria = c.IdCategoria,
                    DataCadastro = c.DataCadastro
                })
                .FirstOrDefault();

            return Json(retorno);
        }

        [HttpPost]
        public virtual ActionResult Excluir(int id) {
            if (!ModelState.IsValid) { return null; }

            var retorno = excluirCommand.Execute(id);

            if (!retorno) { return Json(new { Erro = "Não foi possivel excluir o veículo! Tente novamente" }); }

            _db.SaveChanges();

            return Json(new { Mensagem = "Veículo excluído com sucesso!" });
        }

        public virtual ActionResult ObterCarros(){
            var retorno = _db.Carros
                .Where(c => c.DataExclusao == null)
                .Select(c => new CarroViewModel {
                    Id = c.Id,
                    Modelo = c.Modelo,
                    AnoLancamento = c.AnoLancamento,
                    DataCadastro = c.DataCadastro
                });

            return Json(retorno);
        }

        public virtual ActionResult ObterCategorias(){
            var categorias = _db.Categorias
                .Where(c => c.DataExclusao == null)
                .Select(c => new { Id = c.Id, Nome = c.NomeCategoria });

            return Json(categorias);
        }

        public virtual ActionResult ObterMarcas() {
            var marcas = _db.Marcas
                .Where(c => c.DataExclusao == null)
                .Select(c => new { Id = c.Id, Nome = c.NomeMarca });

            return Json(marcas);
        }
    }
}
