<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Questions.aspx.cs" Inherits="SIGE.Pages.Modulos.Planning.Questions" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <script type="text/javascript">
    
      function pageLoad() {
      }
    
    </script>
    <style type="text/css">
        .style1
        {
            width: 154px;
        }
        
    </style>
    <link href="../../css/backstilo.css" rel="stylesheet" type="text/css" />
</head>
<body class="CargaArchivos">
    <form id="form1" runat="server">
    <div class="CargaArchivos">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <br />
        <table align="center" style="width: 46%; height: 328px;" class="CargaArchivos">
            <tr>
                <td class="style1">
                    <br />
                    <asp:Label ID="lblquestion" runat="server" Text="Pregunta" Font-Bold="True" 
                        Font-Names="Arial" Font-Size="8pt"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtquesion" runat="server" Width="253px" Height="89px" 
                        TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <br />
                    <br />
                    <br />
                </td>
                <td>
                    <asp:RadioButtonList ID="rbltipquesion" runat="server" 
                        RepeatDirection="Horizontal">
                        <asp:ListItem>Abierta</asp:ListItem>
                        <asp:ListItem>Cerrada</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Label ID="lblcountaswers" runat="server" BorderStyle="None" 
                        CssClass="labelsN" Text="No  Respuestas" Font-Bold="True" 
                        Font-Names="Arial" Font-Size="8pt" Width="122px"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server" Width="103px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Label ID="lblaswers" runat="server" Text="Respuestas" Font-Bold="True" 
                        Font-Names="Arial" Font-Size="8pt"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextBox3" runat="server" Width="253px" Height="97px" 
                        TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;</td>
                <td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnaddqasw" runat="server" CssClass="CargaArchivos" 
                        Text="Agregar&gt;&gt;" Width="123px" Height="31px" />
                    &nbsp;</td>
            </tr>
        </table>
        <br />
    </div>
    </form>
</body>
</html>
