using Microsoft.AspNetCore.Mvc;
using ProjetoCarro.Context;
using ProjetoCarro.Context.Commands;
using ProjetoCarro.Models;

namespace ProjetoCarro.Controllers {
    public class MarcaController : Controller {
        private readonly CarroContext _db;
        private readonly SalvarMarcaCommand salvarCommand;
        private readonly ExcluirMarcaCommand excluirCommand;

        public MarcaController(CarroContext db) {
            _db = db;
            salvarCommand = new SalvarMarcaCommand(db);
            excluirCommand = new ExcluirMarcaCommand(db);
        }

        public ActionResult Index() {
            return View();
        }

        [HttpPost]
        public virtual ActionResult Salvar([FromBody] MarcaViewModel marca) {
            if (!ModelState.IsValid) { return null; }

            var retorno = salvarCommand.Execute(marca);

            if (retorno == null) { return Json(new { Erro = "Não foi possivel salvar a marca! Revise as informações e tente novamente" }); }

            _db.SaveChanges();

            return Json(new { Id = retorno.Id, Mensagem = "Marca salva com sucesso!" });
        }

        public virtual ActionResult Editar(int id){
            var retorno = _db.Marcas
                .Where(c => c.Id == id && c.DataExclusao == null)
                .Select(c => new MarcaViewModel {
                    Id = c.Id,
                    NomeMarca = c.NomeMarca,
                    DataCadastro = c.DataCadastro
                })
                .FirstOrDefault();

            return Json(retorno);
        }

        [HttpPost]
        public virtual ActionResult Excluir(int id) {
            if (!ModelState.IsValid) { return null; }

            var retorno = excluirCommand.Execute(id);

            if (!retorno) { return Json(new { Erro = "Não foi possivel excluir a marca. Verifique se existe relacionamentos com `Carro` e tente novamente" }); }

            _db.SaveChanges();

            return Json(new { Mensagem = "Marca excluída com sucesso!" });
        }

        public virtual ActionResult ObterMarcas() {
            var retorno = _db.Marcas
                .Where(c => c.DataExclusao == null)
                .Select(c => new MarcaViewModel {
                    Id = c.Id,
                    NomeMarca = c.NomeMarca,
                    DataCadastro = c.DataCadastro
                });

            return Json(retorno);
        }
    }
}
