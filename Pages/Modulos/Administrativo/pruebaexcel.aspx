<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pruebaexcel.aspx.cs" Inherits="SIGE.Pages.Modulos.Administrativo.pruebaexcel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    <asp:GridView ID="GVExportaExcel" runat="server">
                    </asp:GridView>
                    <%--  <asp:GridView ID="GvCMarcatoExcel" runat="server"
                                                    Visible="False" AutoGenerateColumns="False" >      
                                                                                                   
                                                  <Columns>
                                                    <asp:TemplateField HeaderText="Cod.Marca">                                                      
                                                        <ItemTemplate>
                                                            <asp:Label ID="LblCodBrand2" runat="server" Text='<%# Bind("id_Brand") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="73px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Cliente">                                                        
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblClientecMarca" runat="server" Text='<%# Bind("Company_Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Categoria">
                                                        <ItemTemplate>
                                                        <asp:Label ID="lblidcategoria" runat="server"  Width="80px" Text='<%# Bind("Product_Category") %>'></asp:Label>                                                      
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Marca">                                                        
                                                       <ItemTemplate>
                                                       <asp:Label ID="lblcMArca" runat="server" Width="150px"  Text='<%# Bind("Name_Brand") %>'></asp:Label>                                                                                
                                                       </ItemTemplate>
                                                    </asp:TemplateField>                  
                                                    <asp:TemplateField HeaderText="Estado" >
                                                        <ItemTemplate>
                                                            <asp:label ID="CheckEMarca" runat="server"  Enabled="false" text ='<%# Bind("Brand_Status") %>'  ></asp:label> 
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                                   
                                                </Columns>                                       
                                            </asp:GridView>
                                            
                                           <asp:GridView ID="GVConsulProduct" runat="server" AutoGenerateColumns="False"  
                                               visible="false">               
                                              
                                                <Columns>
                                                 <asp:TemplateField HeaderText="Categoria">                                                        
                                                       <ItemTemplate>
                                                       <asp:label ID="cmbTipoCateg" runat="server" Width="130px" Enabled="false"></asp:label>                                                                                
                                                       </ItemTemplate>
                                                    </asp:TemplateField>                               
                                                       <asp:TemplateField HeaderText="SubCategoria">                                                          
                                                       <ItemTemplate>
                                                       <asp:label ID="CmbSubCategoriaProduct" runat="server" Width="130px"  Enabled="false" ></asp:label>                                                                                
                                                       </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Marca">
                                                        <ItemTemplate>
                                                        <asp:label ID="cmbFabricante" runat="server"  Width="130px" Enabled="false"></asp:label>                                                      
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="SubMarca">
                                                       <ItemTemplate>
                                                       <asp:label ID="cmbSelSubBrand" runat="server" Width="130px" Enabled="false" ></asp:label>                                                                                
                                                       </ItemTemplate>
                                                    </asp:TemplateField> 
                                                     <asp:TemplateField HeaderText="Presentación">                                                       
                                                       <ItemTemplate>
                                                       <asp:label ID="cmbPres" runat="server" Width="180px"   Enabled="false"></asp:label>                                                                                
                                                       </ItemTemplate>
                                                    </asp:TemplateField> 
                                                     <asp:TemplateField HeaderText="Familia">                                                          
                                                       <ItemTemplate>
                                                       <asp:label ID="cmbPFamily" runat="server" Width="150px" Enabled="false" ></asp:label>                                                                                
                                                       </ItemTemplate>
                                                    </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Peso">                                                     
                                                        <ItemTemplate>
                                                            <asp:Label ID="TxtPeso" runat="server" ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SKU">
                                                       <ItemTemplate>
                                                       <asp:Label ID="TxtCodProducto" runat="server" ></asp:Label>
                                                       </ItemTemplate>
                                                       <ItemStyle Width="70px" />
                                                    </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Cliente">                                                          
                                                       <ItemTemplate>
                                                       <asp:Label ID="TxtCompan" runat="server" Width="130px" Enabled="false" ></asp:Label>                                                                                
                                                       </ItemTemplate>
                                                    </asp:TemplateField>  
                                                    <asp:TemplateField HeaderText="Nombre Producto">
                                                        <ItemTemplate>
                                                        <asp:Label ID="TxtNomProducto" runat="server" width="200px" ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                  
                                                    <asp:TemplateField HeaderText="Precio Lista">                                                  
                                                        <ItemTemplate>
                                                          <asp:Label ID="TxtPrecioPDV" runat="server" width="100px" ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Precio PVP">                                                      
                                                        <ItemTemplate>
                                                            <asp:Label ID="TxtPrecioReventa" runat="server" width="100px" ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Estado" >
                                                        <ItemTemplate>
                                                        <asp:label ID="CheckEProducto" runat="server"  Enabled="false"  ></asp:label> 
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                                  
                                                </Columns>                                                
                                            </asp:GridView>--%>
                </td>
            </tr>
        </table>
        <div align="center">
            <asp:ImageButton ID="btn_img_exporttoexcel" runat="server" Height="42px" ImageUrl="~/Pages/Modulos/Operativo/Reports/Image/icono_excel.jpg"
                Width="39px" OnClick="btn_img_exporttoexcel_Click" />
        </div>
    </div>
    </form>
</body>
</html>
