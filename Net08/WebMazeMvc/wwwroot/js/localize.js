﻿$(document).ready(function () {

    const langCookieName = 'lang';

    var cookies = document.cookie.split('; ');
    var filterCookie = cookies.filter(x => x.split('=')[0] == langCookieName);

    if (filterCookie) {
        var currentCookieLang = filterCookie[0].split('=')[1];
        $('.language').val(currentCookieLang);
    }

    $('.language').change(function () {
        var newlang = $('.language').val();
        document.cookie = 'lang=' + newlang;
        location.reload();
    });
    
});