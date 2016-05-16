function showalert(message,alerttype) {
    $('#alert_placeholder').append('<div id="alertdiv" style="z-index: 5000" class="alert fade in alerts ' +  alerttype + '"><a class="close" data-dismiss="alert">×</a><span>'+message+'</span></div>')
    setTimeout(function () {
        $("#alertdiv").remove();
    }, 10000);
}
