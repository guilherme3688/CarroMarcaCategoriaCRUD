$(function () {
    function Marca(id, nome, dataCadastro) {
        var model = this;

        model.Id = ko.observable(id);
        model.NomeMarca = ko.observable(nome);
        model.DataCadastro = ko.observable(dataCadastro);
    }

    function marcaViewModel(model) {
        var model = this;

        // Observables
        model.Id = ko.observable();
        model.NomeMarca = ko.observable();
        model.DataCadastro = ko.observable();
        model.Error = ko.observable();
        model.Success = ko.observable();
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
            model.NomeMarca(null);
            model.DataCadastro(null);
        }
        model.ObterMarcas = function () {
            $.ajax({
                url: '/marca/obtermarcas/',
                type: 'GET',
                contentType: 'application/json; charset=utf-8',
                dataType: "json",
                success: function (retorno) {
                    var marcasMapeadas = retorno.map(function (marca) {
                        let dataFormatada = model._formatarData(marca.dataCadastro);

                        return new Marca(marca.id, marca.nomeMarca, dataFormatada);
                    });

                    model.Marcas(marcasMapeadas);
                }
            });
        };
        model.Editar = function(item) {
            $.ajax({
                url: '/marca/editar/' + item.Id(),
                type: 'GET',
                contentType: 'application/json; charset=utf-8',
                dataType: "json",
                success: function (retorno) {
                    model.Id(retorno.id);
                    model.NomeMarca(retorno.nomeMarca);

                    let dataFormatada = model._formatarData(retorno.dataCadastro);

                    model.DataCadastro(dataFormatada);
                }
            });
        };
        model.Salvar = function() {
            var marca = new Marca(model.Id(), model.NomeMarca());

            $.ajax({
                url: '/marca/salvar',
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                data: ko.toJSON(marca),
                success: function (retorno) {
                    if (retorno.mensagem) {
                        model.Success(retorno.mensagem);
                        marca.Id(retorno.id);

                        model.Editar(marca);
                        model.ObterMarcas();
                    } else if (retorno.erro) {
                        model.Error(retorno.erro);
                        model.Novo();
                    }

                    model._limparMensagens();
                }
            });
        };
        model.Excluir = function (id) {
            if (confirm('Tem certeza que deseja excluir?')) {
                $.ajax({
                    url: '/marca/excluir/' + model.Id(),
                    type: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    dataType: "json",
                    success: function (retorno) {
                        if (retorno.mensagem) {
                            model.Success(retorno.mensagem);
                            model.ObterMarcas();
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
        model.TemRegistros = ko.computed(function () {
            return !!model.Marcas().length > 0;
        });
        model.PodeSalvar = ko.computed(function () {
            return !!model.NomeMarca();
        });
        model.TemMensagem = ko.computed(function () {
            return !!model.Success();
        });
        model.TemErro = ko.computed(function () {
            return !!model.Error();
        });

        // Chamadas
        model.ObterMarcas();
    }

    ko.applyBindings(new marcaViewModel());
});