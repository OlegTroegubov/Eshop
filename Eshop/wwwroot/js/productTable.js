﻿function ajaxRequest(params) {
    var url = 'Product/GetProducts'
    $.get(url + '?' + $.param(params.data)).then(function (res) {
        params.success(res)
        document.getElementById('productTable').style.display = 'inline';
    })
}

var isSorted = false;

function Sorter(sortName, sortOrder) {
    var $table = $('#table');
    var isSorted = $table.data('sorted'); // Получаем значение атрибута

    if (!isSorted) {
        $table.data('sorted', true); // Устанавливаем атрибут в true

        var url = '/Product/GetSortedProducts';
        var data = {
            propertyName: sortName,
            sortOrder: sortOrder,
        };

        $.get(url, data).then(function (data) {
            $('#table').bootstrapTable('load', data);
            $table.data('sorted', false); // Сбрасываем атрибут для разрешения повторной сортировки
        });
    }
}

function totalFormatter() {
    return 'Всего'
}

function nameFormatter(data) {
    return data.length + " продукта"
}

function priceFormatter(data) {
    var field = this.field;
    var total = data.reduce(function (sum, row) {
        var price = parseFloat(row[field].replace('₽', ''));
        return sum + price;
    }, 0);

    return total.toLocaleString('ru-RU', {style: 'currency', currency: 'RUB'});
}

function viewFormatter(value, row) {
    return '<a class="btn btn-primary" href="/Product/Details/' + row.id + '" role="button">'
        + '<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-eye-fill" viewBox="0 0 16 16">'
        + '<path d="M10.5 8a2.5 2.5 0 1 1-5 0 2.5 2.5 0 0 1 5 0z"/>'
        + '<path d="M0 8s3-5.5 8-5.5S16 8 16 8s-3 5.5-8 5.5S0 8 0 8zm8 3.5a3.5 3.5 0 1 0 0-7 3.5 3.5 0 0 0 0 7z"/>'
        + '</svg>'
        + '</a>';
}