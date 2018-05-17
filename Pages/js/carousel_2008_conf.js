/****************************************************************
Carousel de imágenes 2008 1.0. 30-Jul-08
Autor tunait http://javascript.tunait.com/
Script de libre uso mientras se mantengan intactos los créditos de autor.
****************************************************************/

//Inicialicia el/los carousel/es

var carousel1
//var carousel2

function mueveCarousel() {
    carousel1.mueve()
//    carousel2.mueve()
}
onload = function() {
    carousel1 = new tunaCarousel('carousel1', 1, 'ltr')
//    carousel1.controlesCarousel()
//    carousel2 = new tunaCarousel('carousel2', 2, 'ltr')
//    carousel2.controlesCarousel()
    tiempo = setInterval(mueveCarousel, 1)
}
