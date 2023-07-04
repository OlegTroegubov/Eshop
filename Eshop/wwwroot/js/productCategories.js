$(document).ready(function() {
    $.ajax({
        url: '/Product/GetCategories',
        type: 'GET',
        dataType: 'json',
        success: function(data) {
            const categories = data;
            const select = $('#add-category-list, #edit-category-list');
            $.each(categories, function(index, category) {
                const optgroup = $('<optgroup></optgroup>').attr('label', category.name);
                $.each(category.subProductCategories, function(index, subCategory) {
                    const option = $('<option></option>').text(subCategory.name).val(subCategory.id);
                    optgroup.append(option);
                });
                select.append(optgroup);
            });
        },
    });
});
