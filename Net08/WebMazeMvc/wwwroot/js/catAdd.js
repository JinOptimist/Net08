$(document).ready(function () {
    $('.img').hide();
    $('input[name=name]').keyup(function (e) {
        var self = $(this);
        var currentName = self.val();
        $('.img').hide()
        $('.img.loading').show()
        var url = '/cat/IsUniq?name=' + currentName;
        $.get(url)
            .done(function (respone) {
               
                if (respone) {
                    $('.img').hide()
                    $('.img.check').show();
                    //self.css('background', 'green');
                } else {
                    $('.img').hide()
                    $('.img.cancel').show();
                   // self.css('background', 'red');
                }
            });

        //$('body').show();
        //$('body').hide();
    });



});