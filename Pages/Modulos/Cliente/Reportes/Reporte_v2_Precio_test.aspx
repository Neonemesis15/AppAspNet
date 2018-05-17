<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reporte_v2_Precio_test.aspx.cs" Inherits="SIGE.Pages.Modulos.Cliente.Reportes.Reporte_v2_Precio_test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../../css/layout/layout.css" rel="stylesheet" type="text/css" />
    <script src="http://ajax.googleapis.com/ajax/libs/dojo/1.7.1/dojo/dojo.js" type="text/javascript"></script>
    <link rel="stylesheet" href="http://ajax.googleapis.com/ajax/libs/dojo/1.7.1/dijit/themes/claro/claro.css"/>
</head>
<body class="claro">
    <form id="form1" runat="server">
    <div id="wrapper">
        <script src="../../../js/dojo/widgets/menubar.js" type="text/javascript"></script>
        <script src="../../../js/dojo/widgets/layout_tittlepane.js" type="text/javascript"></script>
        <script src="../../../js/dojo/widgets/layout_tabcontainer.js" type="text/javascript"></script>
        <script src="../../../js/charts/tabla.js" type="text/javascript"></script>
        <script src="http://localhost:64319/Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
        <script src="http://localhost:64319/Scripts/jquery-ui-1.8.16.custom.min.js" type="text/javascript"></script>
        <script src="http://localhost:64319/Scripts/json2.js" type="text/javascript"></script>
        <script type="text/javascript">
            $(function () {

                $.ajax({
                    url: "http://localhost:53309/Servicio.svc/getyear",
                    type: "POST",
                    //data: soap,
                    contentType: "text/xml",
                    dataType: "xml",
                    success: function (response) {

//                    $(response).find("string").each(function () { 

//                        var obj = jQuery.parseJSON($(this).text());

//                        } 


//                        var select = $("select#ddl_year");
//                        if (select.prop) { var options = select.prop("options"); }
//                        else { var options = select.attr("options"); }
//                        $("option", select).remove();
//                        options[0] = new Option("Seleccionar...", "-1");
//                        for (var i = 0; i < response.length; i++) {
//                            options[i + 1] = new Option(response[i].Years_id, response[i].Years_Number);
//                        }

//                        var select = $("select#ddl_mes");
//                        if (select.prop) { var options = select.prop("options"); }
//                        else { var options = select.attr("options"); }
//                        $("option", select).remove();
//                        options[0] = new Option("Seleccionar...", "-1");
//                        for (var i = 0; i < response.length; i++) {
//                            options[i + 1] = new Option(response[i].CompanyName, response[i].Companyid);
//                        }
                    },
                    error: function (xhr) {
                        alert("Algo sucedió mal, por favor vuelva a intentarlo." + xhr);
                    }
                });

//                $("select#ddl_company1").change(function (evt) {
//                    if ($("select#ddl_company1").val() != "-1") {
//                        $.ajax({
//                            url: "/Reports/getlistplanning",
//                            type: "POST",
//                            data: { company_id: $("select#ddl_company1").val() },
//                            dataType: "json",
//                            success: function (response) {
//                                var select = $("select#ddl_planning1");
//                                if (select.prop) { var options = select.prop("options"); }
//                                else { var options = select.attr("options"); }
//                                $("option", select).remove();
//                                options[0] = new Option("Seleccionar...", "-1");
//                                for (var i = 0; i < response.length; i++) {
//                                    options[i + 1] = new Option(response[i].PlanningName.substring(0, 43), response[i].idplanning);
//                                }
//                            },
//                            error: function (xhr) {
//                                alert("Algo sucedió mal, por favor vuelva a intentarlo.");
//                            }
//                        });
//                    }
//                });

//                $("select#ddl_planning1").change(function (evt) {
//                    if ($("select#ddl_planning1").val() != "-1") {
//                        $.ajax({
//                            url: "/Reports/getlisttipoagrupcom",
//                            type: "POST",
//                            data: { id_planning: $("select#ddl_planning1").val() },
//                            dataType: "json",
//                            success: function (response) {
//                                var select = $("select#ddl_tipoagrupcom");
//                                if (select.prop) { var options = select.prop("options"); }
//                                else { var options = select.attr("options"); }
//                                $("option", select).remove();
//                                options[0] = new Option("Seleccionar...", "-1");
//                                for (var i = 0; i < response.length; i++) {
//                                    options[i + 1] = new Option(response[i].NodeComType_name, response[i].idNodeComType);
//                                }
//                            },
//                            error: function (xhr) {
//                                alert("Algo sucedió mal, por favor vuelva a intentarlo.");
//                            }
//                        });
//                    }
//                });

//                $("select#ddl_tipoagrupcom").change(function (evt) {
//                    if ($("select#ddl_planning1").val() != "-1") {
//                        $.ajax({
//                            url: "/Reports/getlistagrupcom",
//                            type: "POST",
//                            data: { id_planning: $("select#ddl_planning1").val(), nodecom_type: $("select#ddl_tipoagrupcom").val() },
//                            dataType: "json",
//                            success: function (response) {
//                                var select = $("select#ddl_agrupcomercial");
//                                if (select.prop) { var options = select.prop("options"); }
//                                else { var options = select.attr("options"); }
//                                $("option", select).remove();
//                                options[0] = new Option("Seleccionar...", "-1");
//                                for (var i = 0; i < response.length; i++) {
//                                    options[i + 1] = new Option(response[i].commercialNodeName, response[i].NodeCommercial);
//                                }
//                            },
//                            error: function (xhr) {
//                                alert("Algo sucedió mal, por favor vuelva a intentarlo.");
//                            }
//                        });
//                    }
//                });

//                $("select#ddl_agrupcomercial").change(function (evt) {
//                    if ($("select#ddl_agrupcomercial").val() != "-1") {
//                        $.ajax({
//                            url: "/Reports/getlistpuntosdeventa",
//                            type: "POST",
//                            data: { nodecommercial: $("select#ddl_agrupcomercial").val() },
//                            dataType: "json",
//                            success: function (response) {
//                                $.jStorage.set("pdv_direccion", response);
//                                var select = $("select#ddl_nombrepos");
//                                if (select.prop) { var options = select.prop("options"); }
//                                else { var options = select.attr("options"); }
//                                $("option", select).remove();
//                                options[0] = new Option("Seleccionar...", "-1");
//                                for (var i = 0; i < response.length; i++) {
//                                    options[i + 1] = new Option(response[i].pdv_Name, response[i].id_PointOfsale);
//                                }
//                            },
//                            error: function (xhr) {
//                                alert("Algo sucedió mal, por favor vuelva a intentarlo.");
//                            }
//                        });
//                    }
//                });

//                $("select#ddl_nombrepos").change(function (evt) {
//                    value = $.jStorage.get("pdv_direccion");
//                    if ($("select#ddl_nombrepos").val() != "-1") {
//                        for (var i = 0; i < value.length; i++) {
//                            if ($("select#ddl_nombrepos").val() == value[i].id_PointOfsale)
//                                $("#txt_direccion").val(value[i].pdv_Address)
//                        }
//                    }
//                });

//                $("select#ddl_company").change(function (evt) {
//                    if ($("select#ddl_company").val() != "-1") {
//                        $.ajax({
//                            url: "/Reports/getlistplanning",
//                            type: "POST",
//                            data: { company_id: $("select#ddl_company").val() },
//                            dataType: "json",
//                            success: function (response) {
//                                var select = $("select#ddl_planning");
//                                if (select.prop) { var options = select.prop("options"); }
//                                else { var options = select.attr("options"); }
//                                $("option", select).remove();
//                                options[0] = new Option("Seleccionar...", "-1");
//                                for (var i = 0; i < response.length; i++) {
//                                    options[i + 1] = new Option(response[i].PlanningName.substring(0, 43), response[i].idplanning);
//                                }
//                            },
//                            error: function (xhr) {
//                                alert("Algo sucedió mal, por favor vuelva a intentarlo." + xhr);
//                            }
//                        });
//                    }
//                });
            });  
        </script>
        <div id="header">            
            <img alt="cliente" src="../../../images/layout/logos/logo_alicorp.gif" />
            <div id="menu"></div>
        </div>
        <div id="main">
            <div id="filtros">
            <div id="tabla_filtros">
                <table style="margin:auto">
                    <tr>
                        <td>Año:
                        </td>
                        <td><select id="ddl_year" name="ddl_year"></select>
                        </td>
                        <td>Categoria:
                        </td>
                        <td><select id="ddl_category" name="ddl_category"></select>
                        </td>
                    </tr>
                    <tr>
                        <td>Mes:
                        </td>
                        <td><select id="ddl_month" name="ddl_month"></select>
                        </td>
                        <td>Sub Categoria:
                        </td>
                        <td><select id="ddl_subcategory" name="ddl_subcategory"></select>
                        </td>
                    </tr>
                    <tr>
                        <td>Periodo:
                        </td>
                        <td><select id="ddl_period" name="ddl_period"></select>
                        </td>
                        <td>Marca:
                        </td>
                        <td><select id="ddl_brand" name="ddl_brand"></select>
                        </td>
                    </tr>
                    <tr>
                        <td>Oficina:
                        </td>
                        <td><select id="ddl_oficina" name="ddl_oficina"></select>
                        </td>
                        <td>Sub Marca:
                        </td>
                        <td><select id="ddl_subbrand" name="ddl_subbrand"></select>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>Productos:
                        </td>
                        <td><select id="ddl_products" name="ddl_products"></select>
                        </td>
                    </tr>
                </table>
            </div>
            </div>
            <div style="width: 100%; height: 290px">
                <div id="contenedor">
                    <div id="tab_infoprecios">
                        
                    </div>
                    <div id="tab_quincenal">
                        
                    </div>
                    <div id="tab_margenes">

                    </div>
                </div>
            </div>            
        </div>
        <div id="footer">
        </div>
    </div>
    </form>
</body>
</html>
