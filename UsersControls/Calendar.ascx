<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Calendar.ascx.cs" Inherits="SIGE.UsersControls.Calendar" %>

<asp:Panel ID="Panel1" runat="server" Height="55px" Width="320px">
<table>
<tr>
<td valign="top">


    <asp:Label ID="lbaño" runat="server" ></asp:Label>

</td>
<td valign="top">


    <asp:Label ID="lblmes" runat="server" ></asp:Label>

</td>
<td valign="top">

    <asp:DropDownList ID="cmbaño" runat="server" 
        onselectedindexchanged="cmbaño_SelectedIndexChanged" AutoPostBack="True">
    </asp:DropDownList>

</td>

<td valign="top">

    <asp:DropDownList ID="cmbmes" runat="server" 
        onselectedindexchanged="cmbmes_SelectedIndexChanged" AutoPostBack="True">
    </asp:DropDownList>

</td>


</tr>

</table>


</asp:Panel>
