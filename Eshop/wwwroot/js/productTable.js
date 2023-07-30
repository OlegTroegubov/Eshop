function ajaxRequest(params) {
    $.ajax({
        url: 'Product/GetProducts',
        type: 'GET',
        data: {
            categoryId: $('#select-category-list').val(),
            sortName: params.data.sortName,
            sortOrder: params.data.sortOrder,
        },
        success: function (data) {
            params.success({
                "rows": data
            })
            document.getElementById('productTable').style.display = 'inline';
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log('Error:', errorThrown);
        }
    });
}

var isSorted = false;
function priceSorter(sortName, sortOrder) {
    if (!isSorted) {
        isSorted = true; 

        var params = {
            data: {
                sortName: sortName,
                sortOrder: sortOrder
            },
            success: function (result) {
                $('#table').bootstrapTable('load', result);
                isSorted = false;
            },
        };
        ajaxRequest(params);
    }
}

function handleCategoryChange() {
    $('#table').bootstrapTable('refresh');
}
$('#refreshPage').on('click', function() {
    location.reload(true);
});

$('#select-category-list').on('change', handleCategoryChange);
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