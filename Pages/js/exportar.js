var win=null;

function getExportar(print_area2)
{
 
  win = window.open ("iniio,htm","","location=1,status=1,");
  win.moveTo(0,0);
  win.document.open();
  win.document.writeln(document.getElementById(print_area2).innerHTML); 
  
 
 
  
}
