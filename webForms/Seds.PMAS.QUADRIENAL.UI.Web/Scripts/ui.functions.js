function tabEnter(oEvent) {
    var oEvent = (oEvent) ? oEvent : event;
    var oTarget = (oEvent.target) ? oEvent.target : oEvent.srcElement;
    if (oEvent.keyCode == 13)
        oEvent.keyCode = 9;
    if (oTarget.type == "text" && oEvent.keyCode == 13)    
        oEvent.keyCode = 9;
    if (oTarget.type == "radio" && oEvent.keyCode == 13)
        oEvent.keyCode = 9;
}