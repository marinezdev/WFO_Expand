$(document).ready(function () {
    Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(BeginRequest);
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequest);
    //muestra contenido del asp mostrado
    $('.panelAnimado').each(function () {
        $(this).show("slow");
    });
});

var mpeLoading;
function BeginRequest(sender, args) {

    mpeLoading = $find('idmpeLoading');
    var loc = location.toString();
    mpeLoading.show();
    mpeLoading._backgroundElement.style.zIndex += 10;
    mpeLoading._foregroundElement.style.zIndex += 10;
}
function EndRequest(sender, args) {
    $find('idmpeLoading').hide();

}