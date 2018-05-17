var win=null;

function getPrint(print_area2)
{
  win = window.open("inicio.htm","","fullscreen,");
  self.focus();
  win.document.open();
  win.document.writeln(document.getElementById(print_area2).innerHTML); 

  win.document.close();
  win.print();
  win.close();
}