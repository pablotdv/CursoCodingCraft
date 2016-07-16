function GraficoPaisIndicadorAno(url, indicador, primeiroAno, segundoAno, container) {
    // Load the Visualization API and the piechart package.    
    google.load('visualization', '1.0', { 'packages': ['corechart'] });

    // Set a callback to run when the Google Visualization API is loaded.
    google.setOnLoadCallback(drawChart);

    function drawChart() {
        var jData = "{ indicador: '" + indicador + "', primeiroAno: " + primeiroAno + ", segundoAno: " + segundoAno + "}";

        var jsonData = $.ajax({
            type: 'POST',
            dataType: 'json',
            contentType: 'application/json',
            url: url,
            data: jData,
            async: true,
            beforeSend: function () {
                $("#" + container).html('Carregando gráfico de indicadores por país e ano...');
            },
            success: function (jsonData) {
                // Create our data table out of JSON data loaded from server.
                var data = new google.visualization.DataTable(jsonData);

                var options = {
                    'title': 'GDP (current US$)',
                    bar: { groupWidth: "95%" },
                    animation: { "startup": true },
                    'height': 300,
                    'width': '100%',
                };

                // Instantiate and draw our chart, passing in some options
                var chart = new google.visualization.LineChart(document.getElementById(container));
                chart.draw(data, options);
            }
        }).responseText;


    }
}