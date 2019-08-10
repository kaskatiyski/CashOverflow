function showSidenav() {
    $(".page-wrapper").addClass("toggled");
}

function hideSidenav() {
    $(".page-wrapper").removeClass("toggled");
}

$(document).ready(function () {
    onResize();

    let width = $(window).width();

    $("#close-sidenav").click(function () {
        hideSidenav();
    });

    $("#open-sidenav").click(function () {
        showSidenav();
    });

});

$(window).resize(onResize);

let widthBefore = $(window).width();
let initialResize = false;

function onResize() {
    let width = $(window).width();

    if (width == widthBefore && initialResize) return; 

    initialResize = true;
    widthBefore = $(window).width();

    if (width <= 992) {
        hideSidenav();
    } else {
        showSidenav();
    }
}

