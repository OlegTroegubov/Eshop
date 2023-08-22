function Find(params, data) {
    // If there are no search terms, return all of the data
    if ($.trim(params.term) === '') {
        return data;
    }
    // Do not display the item if there is no 'text' property
    if (typeof data.text === 'undefined') {
        return null;
    }
    // `params.term` should be the term that is used for searching
    // `data.text` is the text that is displayed for the data object
    if (data.text.indexOf(params.term) > -1) {
        var modifiedData = $.extend({}, data, true);
        // You can return modified objects from here
        // This includes matching the `children` how you want in nested data sets
        return modifiedData;
    }
    // Return `null` if the term should not be displayed
    return null;
}

$("#add-category-list").select2({
    placeholder: "Выберите категорию",
    matcher: Find,
    dropdownParent: $('#add-modal-content'),
}).on('select2:select', function (e) {
    const selectedValue = e.params.data.text;
    const trimmedValue = selectedValue.trim();
    $('#select2-add-category-list-container').text(trimmedValue);
});

$("#edit-category-list").select2({
    matcher: Find,
    dropdownParent: $('#edit-modal-content')
}).on('select2:select', function (e) {
    const selectedValue = e.params.data.text;
    const trimmedValue = selectedValue.trim();
    $('#select2-edit-category-list-container').text(trimmedValue);
});

$("#select-category-list").select2({}).on('select2:select', function (e) {
    const selectedValue = e.params.data.text;
    const trimmedValue = selectedValue.trim();
    $('#select2-select-category-list-container').text(trimmedValue);
});

$('#add-category-list').next('.select2-container').css('display', 'block');
$('#edit-category-list').next('.select2-container').css('display', 'block');
$('#select-category-list').next('.select2-container').css('display', 'block');
$('.select2-selection.select2-selection--single').css('height', '36px');
