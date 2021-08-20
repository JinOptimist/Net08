$(document).ready(function () {
    $('.week-event').hide();
    $('.month-event').hide();
    $('.quartal-event').hide();
    $('.year-event').hide();
    $('.text-of-event').hide();
    $('.input-event').hide();
    $('.custom-event').hide();

    $('.dropdown-by-day-of-month').hide();
    $('.dropdown-by-weeks-and-name-of-day').hide();


    $(".event-dropbox").change(function () {
        $('.week-event').hide();
        $('.month-event').hide();
        $('.quartal-event').hide();
        $('.year-event').hide();
        $('.text-of-event').hide();
        $('.input-event').hide();
        $('.custom-event').hide();

        $('.dropdown-by-day-of-month').hide();
        $('.dropdown-by-weeks-and-name-of-day').hide();

        var typeOfEvent = $('.event-dropbox').val();

        switch (typeOfEvent) {
            case 'week':
                $('.week-event').show();
                $('.text-of-event').show();
                $('.input-event').show();
                break;

            case 'month':
                $('.month-event').show();
                $('.dropdown-type-of-month').show();
                $('.dropdown-by-day-of-month').hide();
                $('.dropdown-by-weeks-and-name-of-day').hide();

                $(".dropdown-type-of-month").change(function () {
                    $('.dropdown-by-weeks-and-name-of-day').hide();
                    $('.dropdown-by-day-of-month').hide();
                    $('.text-of-event').hide();
                    $('.input-event').hide();

                    var typeOfMonth = $('.dropdown-type-of-month').val();

                    if (typeOfMonth == 'ByDayOfTheMonth') {
                        $('.dropdown-by-day-of-month').show();
                        $('.text-of-event').show();
                        $('.input-event').show();
                    }
                    else if (typeOfMonth == 'ByWeeksAndNameOfDay') {
                        $('.dropdown-by-weeks-and-name-of-day').show();
                        $('.text-of-event').show();
                        $('.input-event').show();
                    }                    
                });
                break;
            case 'quartal':
                $('.quartal-event').show();
                $('.dropdown-type-of-quartal').show();
                $('.dropdown-by-day-of-quartal').hide();
                $('.by-number-of-mounth').hide();

                $(".dropdown-type-of-quartal").change(function () {
                    $('.dropdown-by-day-of-quartal').hide();
                    $('.by-number-of-mounth').hide();
                    $('.text-of-event').hide();
                    $('.input-event').hide();

                    var typeOfQuartal = $('.dropdown-type-of-quartal').val();

                    if (typeOfQuartal == 'ByDay') {
                        $('.dropdown-by-day-of-quartal').show();
                        $('.text-of-event').show();
                        $('.input-event').show();
                    }
                    else if (typeOfQuartal == 'ByNumberOFMounth') {
                        $('.by-number-of-mounth').show();
                        $('.text-of-event').show();
                        $('.input-event').show();
                    }
                });
                break;
            case 'year':
                $('.year-event').show();
                $('.dropdown-name-of-month').show();
                $('.number-of-days-in-month').hide();
              

                $('.dropdown-name-of-month').change(function () {
                    $('.text-of-event').hide();
                    $('.input-event').hide();
                    $('.number-of-days-in-month').hide();

                    var month = $('.dropdown-name-of-month').val();

                    var url = '/event/DaysInMonth?month=' + month;
                    var promise = $.get(url);

                    $('.number-of-days-in-month').show();
                    $('.text-of-event').show();
                    $('.input-event').show();
                });
                break;
            case 'custom':
                $('.custom-event').show();
                $('.text-of-event').show();
                $('.input-event').show();
                break;
        }
    });
});
