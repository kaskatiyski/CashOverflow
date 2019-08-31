function setPathValue(e) {
    $('#ImagePath').val(e.target.src);
    $('.category-image').removeClass('category-image-active');
    $(e.target).parent().addClass("category-image-active");

    let submitButton = $('#category-submit');

    if (submitButton.is('.btn-success, .btn-danger')) {
        submitButton.removeAttr('disabled');
    }
}

function setType(type) {
    let element = $('#category' + type);
    let label = element.children('.category-type-text');
    let image = element.children('.category-type-image');
    let submitButton = $('#category-submit');

    $('.category-type-text').removeClass('text-dark text-danger text-success');
    $('.category-type-image').removeClass('category-type-active');
    submitButton.removeClass('bg-accent btn-danger btn-success btn-accent');

    if (type == 'Income') {
        label.addClass('text-success');
        submitButton.addClass('btn-success');
    }
    else if (type == 'Expense') {
        label.addClass('text-danger');
        submitButton.addClass('btn-danger');
    }

    if ($('#ImagePath').val()) {
        submitButton.removeAttr('disabled');
    }

    image.addClass('category-type-active');
}