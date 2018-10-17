$(document).ready(function () {

    $('#checkcuando').change(function () {
        if ($(this).is(":checked")) {

            $("#divprogramado").css("display", "none");
            $("#tipoEnvio").val("inmediato");
        }
        else
        {
            $("#divprogramado").css("display", "flex");
            $("#tipoEnvio").val("programado");
        }
    });

    $('#Tipo_Mensaje').change(function () {

        var tipo = document.getElementById("Tipo_Mensaje").value;

        if (tipo == 3)
        {
            $("#divEmail").css("display", "flex");
        }
        else
        {
            $("#divEmail").css("display", "none");
        }
            
        });
        
        
    

});