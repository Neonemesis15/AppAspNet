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

            var errMsg = "Error no controlado en la aplicaci�n de Silverlight " +  appSource + "\n" ;

            errMsg += "C�digo: "+ iErrorCode + "    \n";
            errMsg += "Categor�a: " + errorType + "       \n";
            errMsg += "Mensaje: " + args.ErrorMessage + "     \n";

            if (errorType == "ParserError") {
                errMsg += "Archivo: " + args.xamlFile + "     \n";
                errMsg += "L�nea: " + args.lineNumber + "     \n";
                errMsg += "Posici�n: " + args.charPosition + "     \n";
            }
            else if (errorType == "RuntimeError") {           
                if (args.lineNumber != 0) {
                    errMsg += "L�nea: " + args.lineNumber + "     \n";
                    errMsg += "Posici�n: " +  args.charPosition + "     \n";
                }
                errMsg += "Nombre de m�todo: " + args.methodName + "     \n";
            }

            throw new Error(errMsg);
        }