function ajaxRequest(params) {
    var url = 'Product/GetProducts'
    $.get(url + '?' + $.param(params.data)).then(function (res) {
        params.success(res)
        document.getElementById('productTable').style.display = 'inline';
    })
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

function priceSorter(a, b) {
    var aa = a.replace('₽', '')
    var bb = b.replace('₽', '')
    return aa - bb
}

function viewFormatter(value, row) {
    return '<a class="btn btn-primary mb-2" href="/Product/Details/' + row.id + '" role="button">'
        + '<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-eye-fill" viewBox="0 0 16 16">'
        + '<path d="M10.5 8a2.5 2.5 0 1 1-5 0 2.5 2.5 0 0 1 5 0z"/>'
        + '<path d="M0 8s3-5.5 8-5.5S16 8 16 8s-3 5.5-8 5.5S0 8 0 8zm8 3.5a3.5 3.5 0 1 0 0-7 3.5 3.5 0 0 0 0 7z"/>'
        + '</svg>'
        + '</a>';
}