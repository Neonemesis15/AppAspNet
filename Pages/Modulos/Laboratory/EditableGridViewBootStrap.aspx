<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditableGridViewBootStrap.aspx.cs" Inherits="SIGE.Pages.Modulos.Laboratory.EditableGridViewBootStrap" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <!-- Bootstrap -->
    <link type="text/css" href="../../css/bootstrap.min.css" rel="stylesheet" media="screen" />
    <link type="text/css" href="../../css/bootstrap-theme.min.css" rel="stylesheet" media="screen" />
    <script type="text/javascript" src="../../js/jquery.min.js" ></script>
    <script type="text/javascript" src="../../js/bootstrap.min.js" ></script>

    <!--<link type="text/css" rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.2.0/css/bootstrap.min.css" media="screen"> -->
    <!--<link type="text/css" rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.2.0/css/bootstrap-theme.min.css" media="screen"> -->
    <!--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script> -->
    <!--<script type="text/javascript" src="http://maxcdn.bootstrapcdn.com/bootstrap/3.2.0/js/bootstrap.min.js"></script> -->

    <!-- <script type="text/javascript" src='https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.3.min.js'></script> -->
    <!-- <script type="text/javascript" src='https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js'></script> -->
    <!-- <link rel="stylesheet" href='https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/css/bootstrap.min.css'
        media="screen" /> -->
    <!-- Bootstrap -->
    <form id="form1" runat="server">
        <div style="width: 90%; margin-right: 5%; margin-left: 5%; text-align: center">
            <asp:ScriptManager runat="server" ID="ScriptManager1"/>
            <h1>Grid View System</h1>
            <asp:UpdatePanel ID="upCrudGrid" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="grdvCrudOperation" runat="server" Width="940px" HorizontalAlign="Center"
                        OnRowCommand="GridView1_RowCommand" AutoGenerateColumns="false" AllowPaging="true"
                        DataKeyNames="ID" CssClass="table table-hover table-striped">
                        <Columns>
                            <asp:ButtonField CommandName="detail" 
                                ControlStyle-CssClass="btn btn-info" 
                                ButtonType="Button" Text="Detail" 
                                HeaderText="Detailed View">
                                <ControlStyle CssClass="btn btn-info"></ControlStyle>
                            </asp:ButtonField>
                            <asp:ButtonField CommandName="editRecord" 
                                ControlStyle-CssClass="btn btn-info" 
                                ButtonType="Button" Text="Edit" HeaderText="Edit Record">
                                <ControlStyle CssClass="btn btn-info"></ControlStyle>
                            </asp:ButtonField>
                            <asp:ButtonField CommandName="deleteRecord"
                                ControlStyle-CssClass="btn btn-info" 
                                ButtonType="Button" Text="Delete" 
                                HeaderText="Delete Record">
                               <ControlStyle CssClass="btn btn-info"></ControlStyle>
                            </asp:ButtonField>
                            <asp:BoundField DataField="ID" HeaderText="ID"/>
                            <asp:BoundField DataField="Name" HeaderText="Name"/>
                            <asp:BoundField DataField="EmailID" HeaderText="EmailID"/>
                            <asp:BoundField DataField="Address" HeaderText="Address"/>
                            <asp:BoundField DataField="Contact" HeaderText="Contact NO"/>
                        </Columns>
                    </asp:GridView>
                    <asp:Button ID="btnAdd" runat="server" Text="Add New Record" CssClass="btn btn-info" OnClick="btnAdd_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>

            <div id="detailModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                            <h3 id="myModalLabel">Details</h3>
                        </div>
                        <div class="modal-body">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:DetailsView ID="DetailsView1" runat="server" CssClass="table table-bordered table-hover" 
                                        BackColor="White" ForeColor="Black" FieldHeaderStyle-Wrap="false" 
                                        FieldHeaderStyle-Font-Bold="true" FieldHeaderStyle-BackColor="LavenderBlush" 
                                        FieldHeaderStyle-ForeColor="Black" BorderStyle="Groove" AutoGenerateRows="False">
                                        <Fields>
                                            <asp:BoundField DataField="Name" HeaderText="Name"/>
                                            <asp:BoundField DataField="EmailID" HeaderText="EmailID"/>
                                            <asp:BoundField DataField="Address" HeaderText="Address"/>
                                            <asp:BoundField DataField="Contact" HeaderText="Contact NO"/>
                                        </Fields>
                                    </asp:DetailsView>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="grdvCrudOperation" EventName="RowCommand"/>
                                    <asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click"/>
                                </Triggers>
                            </asp:UpdatePanel>
                            <div class="modal-footer">
                                <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Close</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div id="editModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="editModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                            <h3 id="editModalLabel">Edit Record</h3>
                        </div>
                        <asp:UpdatePanel ID="upEdit" runat="server">
                            <ContentTemplate>
                                <div class="modal-body">
                                    <asp:HiddenField ID="HfUpdateID" runat="server"/>
                                    <table class="table">
                                        <tr>
                                            <td>Name : </td>
                                            <td>
                                                <asp:TextBox ID="txtNameUpdate" runat="server"></asp:TextBox></td>
                                            <td>
                                        </tr>
                                        <tr>    
                                            <td>EmailID</td>
                                            <td>
                                                <asp:TextBox ID="txtEmailIDUpdate" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Address</td>
                                            <td>
                                                <asp:TextBox ID="txtAddressUpdate" runat="server"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td>Contact No</td>
                                            <td>
                                                <asp:TextBox ID="txtContactUpdate" runat="server"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="modal-footer">
                                    <asp:Label ID="lblResult" Visible="false" runat="server"></asp:Label>
                                    <asp:Button ID="btnSave" runat="server" Text="Update" CssClass="btn btn-info" OnClick="btnSave_Click"/>
                                    <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Close</button>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="grdvCrudOperation" EventName="RowCommand"/>
                                <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click"/>
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        


            <div id="addModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="addModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                            <h3 id="addModalLabel">Add New Record</h3>
                        </div>
                        <asp:UpdatePanel ID="upAdd" runat="server">
                            <ContentTemplate>
                                <div class="modal-body">
                                <table class="table table-bordered table-hover">
                                    <tr>     
                                        <td>Name : </td>
                                        <td>
                                            <asp:TextBox ID="txtNameAdd" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>EmailID :</td>
                                        <td>
                                            <asp:TextBox ID="txtEmailIDAdd" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Address:</td>
                                        <td>
                                            <asp:TextBox ID="txtAddressAdd" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Contact No:</td>
                                        <td>
                                            <asp:TextBox ID="txtContactAdd" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="modal-footer">
                                <asp:Button ID="btnAddRecord" runat="server" Text="Add" CssClass="btn btn-info" OnClick="btnAddRecord_Click"/>
                                <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Close</button>
                            </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnAddRecord" EventName="Click"/>
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>

            <div id="deleteModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="delModalLabel" aria-hidden="true">
                 <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                            <h3 id="delModalLabel">Delete Record</h3>
                        </div>
                        <asp:UpdatePanel ID="upDel" runat="server">
                            <ContentTemplate>
                                <div class="modal-body">
                                    Are you sure you want to delete the record?
                                    <asp:HiddenField ID="HfDeleteID" runat="server"/>
                                </div>

                                <div class="modal-footer">
                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn btn-info" OnClick="btnDelete_Click"/>
                                    <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Cancel</button>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnDelete" EventName="Click"/>
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
