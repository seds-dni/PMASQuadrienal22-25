(function () {
    try {
        var n = Sys.Extended.UI.MaskedEditBehavior.prototype, t = n._ExecuteNav;
        n._ExecuteNav = function (n) { var i = n.type; i == "keypress" && (n.type = "keydown"), t.apply(this, arguments), n.type = i }
    }
    catch (i) {
        return
    }
})()