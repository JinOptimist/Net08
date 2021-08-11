$(document).ready(function () {

    $('input[name=name]').keyup(function (e) {
        var self = $(this);
        var currentName = self.val();

        var url = '/cat/IsUniq?name=' + currentName;
        $.get(url)
            .done(function (respone) {
                if (respone) {
                    self.css('border-color', 'green');
                } else {
                    self.css('border-color', 'red');
                }
            });

        //$('body').show();
        //$('body').hide();
    });



});