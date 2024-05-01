//funcion para mensaje de error
//Despues de 5 segundo se cierra el mensaje de error

setTimeout(function () {
    $('.alert').alert('close');
}, 5000);