$(document).ready(function() {
    $.ajax({
        url: '/ProductCategory/GetCategories',
        type: 'GET',
        dataType: 'json',
        success: function(data) {
            const categories = data;
            const select = $('#add-category-list, #edit-category-list');
            $.each(categories, function(index, category) {
                const option = $('<option></option>').text(category.name).val(category.id);
                select.append(option);
            });
        },
    });
});
