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

$('#exampleModalLong').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget) // Button that triggered the modal
    var artId = button.data('articulo') // Extract info from data-* attributes
    var titulo = button.data('titulo') // Extract info from data-* attributes

    var modal = $(this)
    modal.find('.modal-subtitle').text('New message to ' + artId)
    modal.find('.modal-body #artIdRevisar').val(artId)
    modal.find('.modal-body #tituloRevisar').val(titulo)
})

$(document).ready(function () {
    $(document.getElementById('agrega-revisor')).click(function () {
        if ($('.cantidad #todo ul').length < 5) {
            $('#todo').append("<ul>" + $("input[name=task]").val() + " <a href='#' class='close' aria-hidden='true'>&times;</a></ul>");
            console.log($('.cantidad #todo ul').length)
            if ($('.cantidad #todo ul').length > 2) {
                $(document.getElementById('finalizar').disabled = false)
            }
        }
    });

    $("body").on('click', '#todo a', function () {
        $(this).closest("ul").remove();
        console.log($('.cantidad #todo ul').length)
        if ($('.cantidad #todo ul').length < 3) {
            $(document.getElementById('finalizar').disabled = true)
        }
    });
});


$(".revi").click(function () {
    var $fila = $(this).closest("tr");    // Find the row
    var $revisor = $fila.find(".nombre-usuario").text(); // Find the text
    var $boton = $fila.find("#agregar-revisor")

    console.log($fila)

    if ($('.cantidad #todo ul').length < 5) {
        $('#todo').append("<ul>" + $revisor + " <a href='#' class='close' aria-hidden='true'>&times;</a></ul>");
        console.log($('.cantidad #todo ul').length)
        if ($('.cantidad #todo ul').length > 2) {
            $(document.getElementById('finalizar').disabled = false)
        }
        $boton.disabled = true
    }
});

$("body").on('click', '#todo a', function () {
    $(this).closest("ul").remove();
    console.log($('.cantidad #todo ul').length)
    if ($('.cantidad #todo ul').length < 3) {
        $(document.getElementById('finalizar').disabled = true)
    }
});

function retSol(articuloId) {
    $.ajax({
        url: "/Articulos/Revision?envio=ajax&articuloId=" + articuloId,
        type: "GET",
        data: { articuloId },
        dataType: "int",
        async: true,
        cache: false
    });
}