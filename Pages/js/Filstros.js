/*********************************************************************************/
/* Funciones para filtrar el ingreso de caracteres en campos de ingreso de datos */
/*********************************************************************************/
//CreateBy: Ing.Carlos Alberto Hernández Rincón
//CreateDate:04/05/2009
//Requerimiento:
//Permite solo el ingreso de mayúsculas 
function ingresoMayusculas() {
    if ((window.event.keyCode >= 97 && window.event.keyCode <= 122) || window.event.keyCode == 241) {
        window.event.keyCode -= 32;
    }
}

//Permite solo el ingreso de mayusculas
function ingresoSoloMayusculas() {
    if ((window.event.keyCode >= 97 && window.event.keyCode <= 122) || window.event.keyCode == 241 || window.event.keyCode == 154) {
        window.event.keyCode -= 32;
    }
    else if ((window.event.keyCode < 65 || window.event.keyCode > 90) && window.event.keyCode != 32) {
        window.event.keyCode = 0;
    }
}

//Permite solo el ingreso de mayúsculas y dígitos
function ingresoSoloMayusculasYDigitos() {
    if (window.event.keyCode >= 97 && window.event.keyCode <= 122) {
        window.event.keyCode -= 32;
    }
    else if (((window.event.keyCode < 65 || window.event.keyCode > 90) && (window.event.keyCode < 48 || window.event.keyCode > 57)) && window.event.keyCode != 32) {
        window.event.keyCode = 0;
    }
}

//Permite validar direcciones (en mayusculas)
//Permite dígitos y Letras y espacio en blanco 
function ingresoSoloDirecciones() {
    if (window.event.keyCode >= 97 && window.event.keyCode <= 122) {
        window.event.keyCode -= 32;
    }
    else if (((window.event.keyCode < 65 || window.event.keyCode > 90) && (window.event.keyCode < 45 || window.event.keyCode > 57)) && window.event.keyCode != 32) {
        window.event.keyCode = 0;
    }
}

//Permite validar alfanumericos (mayusculas y minusculas)
//incluye guion bajo y alto
function ingresoAlfanumericos() {
    if (((window.event.keyCode < 97 || window.event.keyCode > 122) &&
		(window.event.keyCode < 65 || window.event.keyCode > 90) &&
		(window.event.keyCode < 48 || window.event.keyCode > 57)) && window.event.keyCode != 95 && window.event.keyCode != 45) {
        window.event.keyCode = 0;
    }
}

//Permite solo el ingreso de numeros y punto (.)
function ingresoNumeroDecimales() {
    if (window.event.keyCode != 46 && (window.event.keyCode < 48 || window.event.keyCode > 57)) {
        window.event.keyCode = 0;
    }
}

//Permite solo el ingreso de numeros
function ingresoNumeroEnteroPositivo() {
    if (window.event.keyCode < 48 || window.event.keyCode > 57) {
        window.event.keyCode = 0;
    }
}

//Permite solo el ingreso de numeros con guion
function ingresoNumeroEnteroPositivoGuion() {
    if (window.event.keyCode != 45 && (window.event.keyCode < 48 || window.event.keyCode > 57)) {
        window.event.keyCode = 0;
    }
}


//Permite solo el ingreso de fechas en formato dd/mm/aaaa
function ingresoFecha(o) {
    if (window.event.keyCode < 48 || window.event.keyCode > 57) {
        window.event.keyCode = 0;
        return;
    }
    if (o.value.length == 1 || o.value.length == 4) {
        o.value += String.fromCharCode(window.event.keyCode) + "/";
        window.event.keyCode = 0;
        return;
    }
}

//Permite solo el ingreso de periodos en formatod mm/aaaa
function ingresoPeriodo(o) {
    if (window.event.keyCode < 48 || window.event.keyCode > 57) {
        window.event.keyCode = 0;
        return;
    }
    if (o.value.length == 1) {
        o.value += String.fromCharCode(window.event.keyCode) + "/";
        window.event.keyCode = 0;
        return;
    }
}

function FormatNumber(num, decimalNum, bolLeadingZero, bolParens)
/* IN - num:       the number to be formatted
decimalNum:     the number of decimals after the digit
bolLeadingZero: true / false to use leading zero
bolParens:      true / false to use parenthesis for - num

RETVAL - formatted number
*/
{
    var tmpNum = num;

    // Return the right number of decimal places
    tmpNum *= Math.pow(10, decimalNum);
    tmpNum = Math.floor(tmpNum);
    tmpNum /= Math.pow(10, decimalNum);

    var tmpStr = new String(tmpNum);

    // See if we need to hack off a leading zero or not
    if (!bolLeadingZero && num < 1 && num > -1 && num != 0)
        if (num > 0)
        tmpStr = tmpStr.substring(1, tmpStr.length);
    else
    // Take out the minus sign out (start at 2)
        tmpStr = "-" + tmpStr.substring(2, tmpStr.length);


    // See if we need to put parenthesis around the number
    if (bolParens && num < 0)
        tmpStr = "(" + tmpStr.substring(1, tmpStr.length) + ")";


    return tmpStr;
}
