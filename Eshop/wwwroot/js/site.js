$(function () {
    $('form:not(#deleteForm)').on('submit', function (e) {
        e.preventDefault(); // Предотвращаем отправку формы

        var form = $(this);

        $.ajax({
            url: form.attr('action'),
            type: form.attr('method'),
            data: form.serialize(),
            dataType: 'json',
            success: function (response) {
                // Если ответ успешный, перенаправляем на страницу с деталями
                window.location.href = response.redirectUrl;
            },
            error: function (response) {
                // Если есть ошибки валидации, обновляем форму и отображаем ошибки
                if (response.status === 400) {
                    var errors = response.responseJSON;
                    displayValidationErrors(errors);
                }
            }
        });
    });
});

function displayValidationErrors(errors) {
    // Очищаем предыдущие ошибки
    $('.text-danger').empty();

    // Отображаем новые ошибки
    $.each(errors, function (fieldName, fieldErrors) {
        var errorContainer = $('[data-valmsg-for="' + fieldName + '"]');
        errorContainer.empty(); // Очищаем сообщения об ошибках для данного поля
        $.each(fieldErrors, function (index, errorMessage) {
            errorContainer.append('<span class="text-danger">' + errorMessage + '</span>');
        });
    });
}
