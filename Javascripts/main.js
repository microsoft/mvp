(function ($) {
    $(document).ready(function() {
        $('#container').packery();

        if (window.interactions) {
            window.interactions.setup();
        }
        var pickOne = Math.floor(Math.random() * 3 + 1);
        $('.wrap').addClass('pick-' + pickOne);
    });
}(jQuery));