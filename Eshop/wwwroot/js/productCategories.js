$(document).ready(function () {
    $.ajax({
        url: '/ProductCategory/GetHierarchy',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            const categories = data;
            const select = $('#add-category-list, #edit-category-list');
            const select1 = $('#select-category-list');

            function appendOptions(categories, indentation = "") {
                $.each(categories, function (index, category) {
                    const hasChildren = category.childrenCategories.length > 0;

                    if (hasChildren) {
                        const optgroup = $('<option></option>').html(indentation + category.name).val(category.id).prop('disabled', true);
                        select.append(optgroup);

                        const option = $('<option></option>').html(indentation + category.name).val(category.id);
                        select1.append(option);
                        appendOptions(category.childrenCategories, indentation + "&nbsp;&nbsp;&nbsp;&nbsp;");
                    } else {
                        const option = $('<option></option>').html(indentation + category.name).val(category.id);
                        select.append(option);
                        select1.append($('<option></option>').html(indentation + category.name).val(category.id))
                    }
                });
            }

            appendOptions(categories);
        },
    });
});