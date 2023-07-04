function Find(params, data) {
    // Если нет поисковых терминов, возвращаем все данные
    if ($.trim(params.term) === '') {
        return data;
    }

    // Пропускаем, если отсутствует свойство 'children'
    if (typeof data.children === 'undefined') {
        return null;
    }

    // 'data.children' содержит фактические варианты, с которыми мы сравниваем
    var filteredChildren = [];
    $.each(data.children, function (idx, child) {
        if (child.text.toUpperCase().indexOf(params.term.toUpperCase()) === 0) {
            filteredChildren.push(child);
        }
    });

    // Если были найдены совпадения с дочерними элементами группы часового пояса,
    // тогда устанавливаем найденные дочерние элементы в группе
    // и возвращаем объект группы
    if (filteredChildren.length) {
        var modifiedData = $.extend({}, data, true);
        modifiedData.children = filteredChildren;

        // Можно вернуть модифицированные объекты отсюда
        // Это включает в себя настройку совпадений 'children' во вложенных наборах данных
        return modifiedData;
    }

    // Возвращаем 'null', если термин не должен быть отображен
    return null;
}

$("#add-category-list").select2({
    matcher: Find,
    dropdownParent: $('#add-modal-content')
});
$("#edit-category-list").select2({
    matcher: Find,
    dropdownParent: $('#edit-modal-content')
});
$('#add-category-list').next('.select2-container').css('display', 'block');
$('#edit-category-list').next('.select2-container').css('display', 'block');
$('.select2-selection.select2-selection--single').css('height', '36px');
