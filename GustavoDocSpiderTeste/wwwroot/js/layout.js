var layout = {
    init: function() {

    },
    toggleSidebar: function () {
        const el = $(".title-menu");
        const overlay = $(".overlay");

        this.toggleList(el, "d-none");

        $("#sidebar")
            .toggleClass("side-bar-close")
            .toggleClass("mobile-visible");

        this.toggleList(overlay, "show");
    },
    toggleList: function (el, cl) {
        const total = el.length;

        for (let i = 0; i < total; i++) {
            $(el[i]).toggleClass(cl);
        }
    },
    closeSidebar: function () {
        $("#sidebar").removeClass("mobile-visible");

        const overlay = $(".overlay");
        const total = overlay.length;

        for (let i = 0; i < total; i++) {
            $(overlay[i]).removeClass("show");
        }
    }
};