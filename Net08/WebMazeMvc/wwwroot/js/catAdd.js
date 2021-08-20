$(document).ready(function () {
    $('.icon').hide();

    $('input[name=name]').keyup(function (e) {
        var self = $(this);
        var currentName = self.val();

        var url = '/cat/IsUniq?name=' + currentName;
        var promise = $.get(url);
        promise.done(function (respone) {
                $('.icon').hide();

                if (respone) {
                    $('.icon.ok').show();
                    /*self.css('border-color', 'green');*/
                } else {
                    $('.icon.cancel').show();
                    //self.css('border-color', 'red');
                }
            });

        //$('body').show();
        //$('body').hide();
    });

});