$(document).ready(function () {
    $('.icon-add-cat').hide();

    $('input[name=name]').keyup(function (e) {
        var self = $(this);
        var currentName = self.val();

        var url = '/cat/IsUniq?name=' + currentName;
        $.get(url)
            .done(function (respone) {
                $('.icon-add-cat').hide();
                if (respone) {
                    self.css('border-color', 'green');
                    $('#good').show();
                } else {
                    self.css('border-color', 'red');
                    $('#bad').show();
                }
            });

        //$('body').show();
        //$('body').hide();
    });



});