
$(document).ready(function () {
    
    valor = $("#diverror").html();
    
    if(valor.indexOf("Usuario") > -1)
    {
        $("#diverror").css("display", "block");
    }

});

