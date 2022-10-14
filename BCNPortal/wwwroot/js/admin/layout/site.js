function initLayout() {
    $('.flexslider').flexslider({
        animation: "slide",
        start: function (slider) {
            $('body').removeClass('loading');
        }
    });
    jQuery(document).ready(function ($) {
        $(".scroll").click(function (event) {
            event.preventDefault();
            $('html, body').animate({ scrollTop: $(this.hash).offset().top }, 1200);
        });
    });
    $().UItoTop({ easingType: 'easeOutQuart' });
    new WOW().init(); 
}
