$(function () {
    function Categoria(id, nome, dataCadastro) {
        var model = this;

        model.Id = ko.observable(id);
        model.NomeCategoria = ko.observable(nome);
        model.DataCadastro = ko.observable(dataCadastro);
    }

    function categoriaViewModel(model) {
        var model = this;

        // Observables
        model.Id = ko.observable();
        model.NomeCategoria = ko.observable();
        model.DataCadastro = ko.observable();
        model.Error = ko.observable();
        model.Success = ko.observable();
        model.Categorias = ko.observableArray([]);

        // Methods
        model._limparMensagens = function () {
            setTimeout(function () {
                model.Error(null);
                model.Success(null);
            }, 3000);
        };
        model._formatarData = function (data) {
            let dataMoment = moment(data);
            let dataFormatada = dataMoment.format('DD/MM/YYYY');

            return dataFormatada;
        };
        model.Novo = function () {
            model.Id(null);
            model.NomeCategoria(null);
            model.DataCadastro(null);
        }
        model.ObterCategorias = function() {
            $.ajax({
                url: '/categoria/obtercategorias/',
                type: 'GET',
                contentType: 'application/json; charset=utf-8',
                dataType: "json",
                success: function (retorno) {
                    var categoriasMapeadas = retorno.map(function (categoria) {
                        let dataFormatada = model._formatarData(categoria.dataCadastro);

                        return new Categoria(categoria.id, categoria.nomeCategoria, dataFormatada);
                    });

                    model.Categorias(categoriasMapeadas);
                }
            });
        };
        model.Editar = function(item) {
            $.ajax({
                url: '/categoria/editar/' + item.Id(),
                type: 'GET',
                contentType: 'application/json; charset=utf-8',
                dataType: "json",
                success: function (retorno) {
                    model.Id(retorno.id);
                    model.NomeCategoria(retorno.nomeCategoria);

                    let dataFormatada = model._formatarData(retorno.dataCadastro);

                    model.DataCadastro(dataFormatada);
                }
            });
        };
        model.Salvar = function() {
            var categoria = new Categoria(model.Id(), model.NomeCategoria());

            $.ajax({
                url: '/categoria/salvar',
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                data: ko.toJSON(categoria),
                success: function (retorno) {
                    if (retorno.mensagem) {
                        model.Success(retorno.mensagem);

                        model.Id(retorno.id);
                        model.ObterCategorias();
                    } else if(retorno.erro) {
                        model.Error(retorno.erro);
                        model.Limpar();
                    }

                    model._limparMensagens();
                }
            });
        };
        model.Excluir = function() {
            if(confirm('Tem certeza que deseja excluir?')) {
                $.ajax({
                    url: '/categoria/excluir/' + model.Id(),
                    type: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: "json",
                    success: function (retorno) {
                        if (retorno.mensagem) {
                            model.Success(retorno.mensagem);
                            model.ObterCategorias();
                        } else if (retorno.erro) {
                            model.Error(retorno.erro);
                        }

                        model._limparMensagens();
                        model.Novo();
                    }
                });
            }

            return;
        };

        // Computeds
        model.TemId = ko.computed(function() {
            return !!model.Id();
        });
        model.TemRegistros = ko.computed(function() {
            return !!model.Categorias().length > 0;
        });
        model.PodeSalvar = ko.computed(function() {
            return !!model.NomeCategoria();
        });
        model.TemMensagem = ko.computed(function() {
            return !!model.Success();
        });
        model.TemErro = ko.computed(function() {
            return !!model.Error();
        });

        // Chamadas
        model.ObterCategorias();
    }

    ko.applyBindings(new categoriaViewModel());
});