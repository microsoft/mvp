(function ($) {
    $(document).ready(function() {
        $('#container').packery();

        if (window.interactions) {
            window.interactions.setup();
        }
    });
}(jQuery));