var animate_top = false, animate_bottom = false;
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
        if (animate_top == false) {
            $('body, html').animate({ scrollTop: 0 }, 800);
            animate_top = true;
        }
        else {
            $('body, html').stop(true, false);
            animate_top = false;
        }
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
        if (animate_bottom == false) {
            $('body, html').animate({ scrollTop: $(document).height() }, 800);
            animate_bottom = true;
        }
        else {
            $('body, html').stop(true, false);
            animate_bottom = false;
        }
    });
});