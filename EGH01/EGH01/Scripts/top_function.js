$(function () {
    $(window).scroll(function () {
        if ($(this).scrollTop() != 0) {
            $('#toTop').fadeIn();
        }
        else {
            $('#toTop').fadeOut();
        }
    });
    $('#toTop').click(function () {
        $('body, html').animate({ scrollTop: 0 }, 800)
    });
});
$(function () {
    $(window).scroll(function () {
        if ($(this).scrollTop() != $(document).height()) {
            $('#toBot').fadeIn();
        }
        else {
            $('#toBot').fadeOut();
        }
    });
    $('#toBot').click(function () {
        $('body, html').animate({ scrollTop: $(document).height() }, 800)
    });
});