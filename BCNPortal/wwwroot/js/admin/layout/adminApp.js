var adminApp = function () {

    $(function () {
        toggleSettings();
        navToggleSub();
        navToggleLeft();
        switchTheme();
        switcheryToggle();
        navToggleRight();
        fullscreenWidget();
    });

    var toggleSettings = function () {
        $('.config-link').click(function () {
            if ($(this).hasClass('open')) {
                $('#config').animate({
                    "right": "-205px"
                }, 150);
                $(this).removeClass('open').addClass('closed');
            } else {
                $("#config").animate({
                    "right": "0px"
                }, 150);
                $(this).removeClass('closed').addClass('open');
            }
        });
    };

    var toggleSettings = function () {
        $('.config-link').click(function () {
            if ($(this).hasClass('open')) {
                $('#config').animate({
                    "right": "-205px"
                }, 150);
                $(this).removeClass('open').addClass('closed');
            } else {
                $("#config").animate({
                    "right": "0px"
                }, 150);
                $(this).removeClass('closed').addClass('open');
            }
        });
    };

    var switchTheme = function () {
        $('.theme-style-wrapper').click(function () {
            $('#main-wrapper').attr('class', '');
            var themeValue = $(this).data('theme');
            $('#main-wrapper').addClass(themeValue);
        });
    };


    var navToggleRight = function () {
        $('#toggle-right').on('click', function () {
            $('#sidebar-right').toggleClass('sidebar-right-open');
            $("#toggle-right .fa").toggleClass("fa-indent fa-dedent");

        });
    };

    var customCheckbox = function () {
        $('input.icheck').iCheck({
            checkboxClass: 'icheckbox_flat-grey',
            radioClass: 'iradio_flat-grey'
        });
    }

  

    var formWizard = function () {
        $('#myWizard').wizard();
    }

    var navToggleLeft = function () {
        $('#toggle-left').on('click', function () {
            var bodyEl = $('#main-wrapper');
            ($(window).width() > 767) ? $(bodyEl).toggleClass('sidebar-mini') : $(bodyEl).toggleClass('sidebar-opened');
        });
    };

    var navToggleSub = function () {
        var subMenu = $('.sidebar .nav');
        $(subMenu).navgoco({
            caretHtml: false,
            accordion: true
        });

    };

    var profileToggle = function () {
        $('#toggle-profile').click(function () {
            $('.sidebar-profile').slideToggle();
        });
    };

    var widgetToggle = function () {
        $(".actions > .fa-chevron-down").click(function () {
            $(this).parent().parent().next().slideToggle("fast"), $(this).toggleClass("fa-chevron-down fa-chevron-up");
        });
    };

    var widgetClose = function () {
        $(".actions > .fa-times").click(function () {
            $(this).parent().parent().parent().fadeOut();
        });
    };

    var widgetFlip = function () {
        $(".actions > .fa-cog").click(function () {
            $(this).closest('.flip-wrapper').toggleClass('flipped')
        });
    };

    var dateRangePicker = function () {
        $('.reportdate').daterangepicker({
            format: 'YYYY-MM-DD',
            startDate: '2014-01-01',
            endDate: '2014-06-30'
        });
    };



    var spinStart = function (spinOn) {
        var spinFull = $('<div class="preloader"><div class="iconWrapper"><i class="fa fa-circle-o-notch fa-spin"></i></div></div>');
        var spinInner = $('<div class="preloader preloader-inner"><div class="iconWrapper"><i class="fa fa-circle-o-notch fa-spin"></i></div></div>');
        if (spinOn === undefined) {
            $('body').prepend(spinFull);
        } else {
            $(spinOn).prepend(spinInner);
        };

    };


    var spinStop = function () {
        $('.preloader').remove();
    };

    var switcheryToggle = function () {
        var elems = Array.prototype.slice.call(document.querySelectorAll('.js-switch'));
        elems.forEach(function (html) {
            var switchery = new Switchery(html, {
                size: 'small',
                color: '#27B6AF',
                secondaryColor: '#B3B8C3'
            });
        });
    };

    var fullscreenWidget = function () {
        $('.panel .fa-expand').click(function () {
            var panel = $(this).closest('.panel');
            panel.toggleClass('widget-fullscreen');
            $(this).toggleClass('fa-expand fa-compress');
            $('body').toggleClass('fullscreen-widget-active');

        })
    };


    var fullscreenMode = function () {
        $('#toggle-fullscreen.expand').on('click', function () {
            $(document).toggleFullScreen();
            $('#toggle-fullscreen .fa').toggleClass('fa-expand fa-compress');
        });
    };



    //return functions
    return {
        dateRangePicker: dateRangePicker,
        customCheckbox: customCheckbox,
        spinStart: spinStart,
        spinStop: spinStop
    };
}();