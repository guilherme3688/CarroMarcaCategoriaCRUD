$(function () {
    function Carro(id, modelo, ano, idMarca, idCategoria, dataCadastro) {
        var model = this;

        model.Id = ko.observable(id);
        model.Modelo = ko.observable(modelo);
        model.AnoLancamento = ko.observable(ano);
        model.IdMarca = ko.observable(idMarca);
        model.IdCategoria = ko.observable(idCategoria);
        model.DataCadastro = ko.observable(dataCadastro);
    }

    function carroViewModel(model) {
        var model = this;

        // Observables
        model.Id = ko.observable();
        model.Modelo = ko.observable();
        model.AnoLancamento = ko.observable();
        model.DataCadastro = ko.observable();
        model.IdMarca = ko.observable();
        model.IdCategoria = ko.observable();
        model.Error = ko.observable();
        model.Success = ko.observable();
        model.Carros = ko.observableArray([]);
        model.Categorias = ko.observableArray([]);
        model.Marcas = ko.observableArray([]);

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
            model.Modelo(null);
            model.AnoLancamento(null);
            model.IdMarca(null);
            model.IdCategoria(null);
            model.DataCadastro(null);
        }
        model.ObterCarros = function() {
            $.ajax({
                url: '/carro/obtercarros/',
                type: 'GET',
                contentType: 'application/json; charset=utf-8',
                dataType: "json",
                success: function (retorno) {
                    var carrosMapeados = retorno.map(function (carro) {
                        let dataFormatada = model._formatarData(carro.dataCadastro);

                        return new Carro(carro.id, carro.modelo, carro.anoLancamento, carro.idMarca, carro.idCategoria, dataFormatada);
                    });

                    model.Carros(carrosMapeados);
                }
            });
        };
        model.ObterCategorias = function () {
            $.ajax({
                url: '/carro/obtercategorias/',
                type: 'GET',
                contentType: 'application/json; charset=utf-8',
                dataType: "json",
                success: function (retorno) {
                    model.Categorias(retorno);
                }
            });
        };
        model.ObterMarcas = function () {
            $.ajax({
                url: '/carro/obtermarcas/',
                type: 'GET',
                contentType: 'application/json; charset=utf-8',
                dataType: "json",
                success: function (retorno) {
                    model.Marcas(retorno);
                }
            });
        };
        model.Editar = function(item) {
            $.ajax({
                url: '/carro/editar/' + item.Id(),
                type: 'GET',
                contentType: 'application/json; charset=utf-8',
                dataType: "json",
                success: function (retorno) {
                    model.Id(retorno.id);
                    model.Modelo(retorno.modelo);
                    model.AnoLancamento(retorno.anoLancamento);
                    model.IdMarca(retorno.idMarca);
                    model.IdCategoria(retorno.idCategoria);

                    let dataFormatada = model._formatarData(retorno.dataCadastro);

                    model.DataCadastro(dataFormatada);
                }
            });
        };
        model.Salvar = function () {
            var carro = new Carro(model.Id(), model.Modelo(), model.AnoLancamento(), model.IdMarca(), model.IdCategoria());

            $.ajax({
                url: '/carro/salvar',
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                data: ko.toJSON(carro),
                success: function (retorno) {
                    if (retorno.mensagem) {
                        model.Success(retorno.mensagem);
                        carro.Id(retorno.id);

                        model.Editar(carro);
                        model.ObterCarros();
                    } else if (retorno.erro) {
                        model.Error(retorno.erro);
                        model.Novo();
                    }

                    model._limparMensagens();
                }
            });
        };
        model.Excluir = function () {
            if (confirm('Tem certeza que deseja excluir?')) {
                $.ajax({
                    url: '/carro/excluir/' + model.Id(),
                    type: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: "json",
                    success: function (retorno) {
                        if (retorno.mensagem) {
                            model.Success(retorno.mensagem);
                            model.ObterCarros();
                        } else if (retorno.erro) {
                            model.Error(retorno.erro);
                        }

                        model._limparMensagens();
                        model.Novo();
                    }
                });

                return;
            }
        };

        // Computeds
        model.TemId = ko.computed(function () {
            return !!model.Id();
        });
        model.TemRegistros = ko.computed(function () {
            return !!model.Carros().length > 0;
        });
        model.PodeSalvar = ko.computed(function () {
            return !!model.Modelo() && !!model.AnoLancamento() && !!model.IdMarca() && !!model.IdCategoria();
        });
        model.TemMensagem = ko.computed(function () {
            return !!model.Success();
        });
        model.TemErro = ko.computed(function () {
            return !!model.Error();
        });

        // Chamadas
        model.ObterCategorias();
        model.ObterMarcas();
        model.ObterCarros();
    }

    ko.applyBindings(new carroViewModel());
});