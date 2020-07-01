// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
$('#exampleModal').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget) // Button that triggered the modal
    var artId = button.data('articulo') // Extract info from data-* attributes
    var titulo = button.data('titulo') // Extract info from data-* attributes
    // If necessary, you could initiate an AJAX request here (and then do the updating in a callback).
    // Update the modal's content. We'll use jQuery here, but you could use a data binding library or other methods instead.
    var modal = $(this)
    modal.find('.modal-subtitle').text('New message to ' + artId)
    modal.find('.modal-body #artId').val(artId)
    modal.find('.modal-body #titulo').val(titulo)

})

$('#modalAceptarRechazar').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget) // Button that triggered the modal
    var artId = button.data('articulo') // Extract info from data-* attributes
    var titulo = button.data('titulo') // Extract info from data-* attributes
    var estado = button.data('estado') // Extract info from data-* attributes
    // If necessary, you could initiate an AJAX request here (and then do the updating in a callback).
    // Update the modal's content. We'll use jQuery here, but you could use a data binding library or other methods instead.
    var modal = $(this)
    modal.find('.modal-body #artId').val(artId)
    modal.find('.modal-body #titulo').val(titulo)
    modal.find('.modal-body #estado').val(estado)
})

$('#modalRevisionFinal').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget) // Button that triggered the modal
    var artId = button.data('articulo') // Extract info from data-* attributes
    var titulo = button.data('titulo') // Extract info from data-* attributes
    var estado = button.data('estado') // Extract info from data-* attributes
    var autoresString = button.data('autores') // Extract info from data-* attributes
    var comentarios = button.data('comments')
    var puntuacion = button.data('puntos')
    // Extract info from data-* attributes
    // If necessary, you could initiate an AJAX request here (and then do the updating in a callback).
    // Update the modal's content. We'll use jQuery here, but you could use a data binding library or other methods instead.
    var modal = $(this)

    modal.find('.modal-body #artId').val(artId)
    modal.find('.modal-body #titulo').val(titulo)
    modal.find('.modal-body #estado').val(estado)
    modal.find('.modal-body #autoresString').val(autoresString)
    modal.find('.modal-body #comentarios').val(comentarios)
    modal.find('.modal-body #puntuacion').val(puntuacion)
})

$(document).ready(function () {
    $("body").on('click', '#todo a', function () {
        var $fila = $(this).closest("ul");   

        $(this).closest("ul").remove();

        if ($('.cantidad #todo ul').length < 3) {
            $(document.getElementById('finalizar').disabled = true)
            
        }
        $listaAsignados.pop()
        console.log($fila)
        console.log($listaAsignados)
    });
});

var $lista = "";

var $listaAsignados = [];

$(".revi").click(function () {
    var $fila = $(this).closest("tr");    // Find the row
    var $revisor = $fila.find(".nombre-usuario").text(); // Find the text
    var $boton = $fila.find("#agregar-revisor")

    if ($('.cantidad #todo ul').length < 5 && $listaAsignados.indexOf($revisor) == -1) {
        $listaAsignados.push($revisor)
        console.log($listaAsignados)
        
        $('#todo').append("<ul id='item-revisor'>" + $revisor + " <a href='#' class='close' aria-hidden='true'>&times;</a></ul>");

        if ($('.cantidad #todo ul').length > 2) {
            $(document.getElementById('finalizar').disabled = false)
        }
        $boton.disabled = true
    }
});

$('#finalizar').click(function () {
    var revisores = $(document.getElementById('lista-revisores'));

    var $temp = ""
    for (let i = 0; i < $listaAsignados.length; i++) {
        $temp += $listaAsignados[i] + ","
    }

    revisores.val($temp)

    console.log($temp)

});