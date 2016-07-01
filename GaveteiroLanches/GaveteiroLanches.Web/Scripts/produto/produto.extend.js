jQuery.fn.extend({
    produto: function (settings, callbackFnk) {

        if (settings == null) {
            settings = {
                urlDescricao: "",
                urlId: "",
                produtoId: null,
                select: null
            };
        }

        var elemento = this;

        this.autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: settings.urlDescricao,
                    data: "{'descricao': '" + request.term + "'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataFilter: function (data) { return data; },
                    success: function (data) {
                        response($.map(data, function (item) {
                            return {
                                value: item.Descricao,

                                ProdutoId: item.ProdutoId,
                                Descricao: item.Descricao,
                                ValorUnitario: item.ValorUnitario,
                            }
                        }));
                    }
                });
            },

            select: function (event, ui) {
                var item = ui.item;
                if (item) {
                    $(elemento).val(item.Descricao);

                    $(settings.produtoId).val(item.ProdutoId);

                    $(elemento).change();

                    if (typeof callbackFnk == 'function')
                        callbackFnk.call(this, item);
                }
            },

            change: function (event, ui) {
                var item = ui.item;
                if (item) {
                    $(elemento).val(item.Descricao);
                }
            }
        });

        $(settings.produtoId).ready(function () {
            $(settings.produtoId).change();
        });

        $(settings.produtoId).on("change",
            function (request, response) {
                $.ajax({
                    url: settings.urlId,
                    data: "{'produtoId': '" + this.value + "'}",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    dataFilter: function (data) { return data; },
                    success: function (data) {
                        var item = data;

                        if (item != null) {
                            $(elemento).val(item.Descricao).change();
                            $(settings.produtoId).val(item.ProdutoId);

                            if (typeof callbackFnk == 'function')
                                callbackFnk.call(this, item);
                        }
                    }
                })
            });
    }
});