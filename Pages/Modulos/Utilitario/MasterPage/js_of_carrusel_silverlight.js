function onSilverlightError(sender, args) {
    var appSource = "";
    if (sender != null && sender != 0) {
        appSource = sender.getHost().Source;
    }

    var errorType = args.ErrorType;
    var iErrorCode = args.ErrorCode;

    if (errorType == "ImageError" || errorType == "MediaError") {
        return;
    }

    var errMsg = "Error no controlado en la aplicación de Silverlight " + appSource + "\n";

    errMsg += "Código: " + iErrorCode + "    \n";
    errMsg += "Categoría: " + errorType + "       \n";
    errMsg += "Mensaje: " + args.ErrorMessage + "     \n";

    if (errorType == "ParserError") {
        errMsg += "Archivo: " + args.xamlFile + "     \n";
        errMsg += "Línea: " + args.lineNumber + "     \n";
        errMsg += "Posición: " + args.charPosition + "     \n";
    }
    else if (errorType == "RuntimeError") {
        if (args.lineNumber != 0) {
            errMsg += "Línea: " + args.lineNumber + "     \n";
            errMsg += "Posición: " + args.charPosition + "     \n";
        }
        errMsg += "Nombre de método: " + args.methodName + "     \n";
    }

    throw new Error(errMsg);
}