(function () {


    $('#form-confirma')
        .on('submit',
            function () {
                if (!$(this).valid()) {
                    return false;
                }

                $('#form-confirma button[type=submit]').prop('disabled', true);

                return true;
            });

})();