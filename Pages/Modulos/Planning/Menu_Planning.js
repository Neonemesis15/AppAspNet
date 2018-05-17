


function btnloadcontrupla_onclick() {


      ret = PgeLoad.LoadPage("ini_planning.aspx", OnComplete, OnTimeOut, OnError);
//    ret = open("ini_planning.aspx");
    document.getElementById('Loading').style.display = 'block';
    return (true);
}
function btnasigcanal_onclick() {
    ret = PgeLoad.LoadPage("~/AsignacionesxCanal.aspx", OnComplete, OnTimeOut, OnError);
    
    document.getElementById('Loading').style.display = 'block';
    return (true);
}

function OnComplete(args) {
    document.getElementById('Target').innerHTML = args;
}

function OnTimeOut(args) {
    alert("Service call timed out.");
}

function OnError(args) {
    alert("Error calling service method.");
}
