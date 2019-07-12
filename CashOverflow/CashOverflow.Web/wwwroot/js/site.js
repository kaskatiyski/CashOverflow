function showSidenav() {
    $(".page-wrapper").addClass("toggled");
}

function hideSidenav() {
    $(".page-wrapper").removeClass("toggled");
}

$(document).ready(function () {
    onResize();

    $("#close-sidenav").click(function() {
        hideSidenav();
    });

    $("#open-sidenav").click(function() {
        showSidenav();
    });

});

$(window).resize(onResize);

function onResize() {
    let width = $(window).width();

    if (width <= 992) {
        hideSidenav();
    } else {
        showSidenav();
    }
}

