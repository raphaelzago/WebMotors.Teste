$(function () {
    $("#MarcaId").change(function () {
        let marcaId = $(this).val();
        let urlModelos = "/Anuncio/ObterModelos?MarcaId=" + marcaId;

        $.getJSON(urlModelos, function (data) {
            var select = $("#ModeloId");
            select.empty();

            select.append($("<option />").val(-1).text("Selecione..."));
            $.each(data.modelos, function (key, modelo) {
                select.append($("<option />").val(modelo.id).text(modelo.name));
            });
        });

        $("#Marca").val(($("#MarcaId option:selected").text()));

        var selectVersao = $("#VersaoId");
        selectVersao.empty();
        selectVersao.append($("<option />").val(-1).text("Selecione..."));

        var inputVersao = $("#Versao");
        inputVersao.val("");

        var inputModelo = $("#Modelo");
        inputModelo.val("");
    });

    $("#ModeloId").change(function () {
        let modeloId = $(this).val();
        let urlVersao = "/Anuncio/ObterVersoes?ModeloId=" + modeloId;
        var inputVersao = $("#Versao");
        inputVersao.val("");

        $.getJSON(urlVersao, function (data) {
            var select = $("#VersaoId");
            select.empty();

            select.append($("<option />").val(-1).text("Selecione..."));
            $.each(data.versoes, function (key, versao) {
                select.append($("<option />").val(versao.id).text(versao.name));
            });
        });

        $("#Modelo").val(($("#ModeloId option:selected").text()));
    });

    $("#VersaoId").change(function () {
        $("#Versao").val(($("#VersaoId option:selected").text()));
    });
});