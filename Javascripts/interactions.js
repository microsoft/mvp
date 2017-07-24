var interactions = (function () {
    var hitBottom = false,
        hasScrolled = false,
        hasTrackedScroll = false,
        interval, mod = {};

    function scrollListener() {
        hasScrolled = true;
    }

    function scrollInterval() {
        // Fixed error when appInsights is not defined
        if (typeof appInsights === 'undefined' || !appInsights || !hasScrolled) {
            return;
        }

        if (!hasTrackedScroll) {
            hasTrackedScroll = true;
            appInsights.trackEvent('Page:Scrolled');
        }

        if (!hitBottom && ((window.innerHeight + window.scrollY) >= document.body.scrollHeight)) {
            hitBottom = true;
            appInsights.trackEvent('Page:End');

            if (interval) {
                window.clearInterval(interval);
            }
            window.removeEventListener('scroll', scrollListener);

        }

        hasScrolled = false;
    }

    mod.setup = function () {
        window.addEventListener('scroll', scrollListener);
        interval = window.setInterval(scrollInterval, 100);

        document.body.addEventListener('copy', function (e) {
            appInsights.trackEvent('Clipboard:Copy');
        });
        document.body.addEventListener('cut', function (e) {
            appInsights.trackEvent('Clipboard:Cut');
        });
    }

    return mod;
}());
