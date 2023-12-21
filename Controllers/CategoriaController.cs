using Microsoft.AspNetCore.Mvc;
using ProjetoCarro.Context;
using ProjetoCarro.Context.Commands;
using ProjetoCarro.Models;

namespace ProjetoCarro.Controllers {
    public class CategoriaController : Controller {
        private readonly CarroContext _db;
        private readonly SalvarCategoriaCommand salvarCommand;
        private readonly ExcluirCategoriaCommand excluirCommand;

        public CategoriaController(CarroContext db) {
            _db = db;
            salvarCommand = new SalvarCategoriaCommand(db);
            excluirCommand = new ExcluirCategoriaCommand(db);
        }

        public ActionResult Index() {
            return View();
        }

        [HttpPost]
        public virtual ActionResult Salvar([FromBody] CategoriaViewModel categoria) {
            if (!ModelState.IsValid) { return null; }

            var retorno = salvarCommand.Execute(categoria);

            if (retorno == null) { return Json(new { Erro = "Não foi possivel salvar a categoria! Revise as informações e tente novamente" }); }

            _db.SaveChanges();

            return Json(new { Id = retorno.Id, Mensagem = "Categoria salva com sucesso!" });
        }

        public virtual ActionResult Editar(int id){
            var retorno = _db.Categorias
                .Where(c => c.Id == id && c.DataExclusao == null)
                .Select(c => new CategoriaViewModel {
                    Id = c.Id,
                    NomeCategoria = c.NomeCategoria,
                    DataCadastro = c.DataCadastro
                })
                .FirstOrDefault();

            return Json(retorno);
        }

        [HttpPost]
        public virtual ActionResult Excluir(int id) {
            if (!ModelState.IsValid) { return null; }

            var retorno = excluirCommand.Execute(id);

            if (!retorno) { return Json(new { Erro = "Não foi possivel excluir a categoria. Verifique se existe relacionamentos com `Carro` e tente novamente" }); }

            _db.SaveChanges();

            return Json(new { Mensagem = "Categoria excluída com sucesso!" });
        }

        public virtual ActionResult ObterCategorias() {
            var retorno = _db.Categorias
                .Where(c => c.DataExclusao == null)
                .Select(c => new CategoriaViewModel {
                    Id = c.Id,
                    NomeCategoria = c.NomeCategoria,
                    DataCadastro = c.DataCadastro
                });

            return Json(retorno);
        }
    }
}
