<html>
<head>
    <link href="StiloAdministrativo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <div style="display:table;">
        <div style="display:table-row;">
            <div style="display:inline;">
                <div class="fila">
                <div class="celda">
                    <fieldset id="FlsInfClasifProd" runat="server" style="height: 113px;">
                        <legend style="">Clasificación</legend>
                        <div class="tabla">
                            <div class="fila">
                                <div class="celda">
                                    <asp:Label ID="LblSelCateg" runat="server" CssClass="labels" Text="Categoria *"></asp:Label>
                                </div>
                                <div class="celda">
                                    <asp:DropDownList ID="cmbTipoCateg" runat="server" AutoPostBack="True" Width="170px"
                                        Enabled="False" OnSelectedIndexChanged="cmbTipoCateg_SelectedIndexChanged" Style="margin-top: 2px">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="fila">
                                <div class="celda">
                                    <asp:Label ID="LblSubcategoria" runat="server" CssClass="labels" Text="SubCategoria"></asp:Label>
                                </div>
                                <div class="celda">
                                    <asp:DropDownList ID="CmbSubCategoriaProduct" runat="server" AutoPostBack="True"
                                        Width="170px" Enabled="False" OnSelectedIndexChanged="CmbSubCategoriaProduct_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="fila">
                                <div class="celda">
                                    <asp:Label ID="LblPFamily" runat="server" CssClass="labels" Text="Familia "></asp:Label>
                                </div>
                                <div class="celda">
                                    <asp:DropDownList ID="cmbPFamily" runat="server" CausesValidation="True" Width="170px"
                                        Enabled="False">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="fila">
                                <div class="celda">
                                    <asp:Label ID="LblSelFabricante" runat="server" CssClass="labels" Text="Marca*"></asp:Label>
                                </div>
                                <div class="celda">
                                    <asp:DropDownList ID="cmbFabricante" runat="server" Width="170px" Enabled="False">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </fieldset>
                    &nbsp;
                </div>
            </div>
                <div class="fila">
                <div class="celda">
                    <fieldset id="Fieldset3" runat="server" style="height: 113px;">
                        <legend style="">Especificaciones</legend>
                        <div class="tabla">
                            <div class="fila">
                                <div class="celda">
                                    <asp:Label ID="lblEspTipo" runat="server" CssClass="labels" Text="Tipo"></asp:Label>
                                </div>
                                <div class="celda">
                                    <asp:DropDownList ID="cmbTipoProducto" runat="server" Width="170px" Enabled="False"
                                        Style="margin-top: 2px">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="fila">
                                <div class="celda">
                                    <asp:Label ID="LblEspFormato" runat="server" CssClass="labels" Text="Formato____"></asp:Label>
                                </div>
                                <div class="celda">
                                    <asp:DropDownList ID="cmbFormatoProducto" runat="server" Width="170px" Enabled="False">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </fieldset>
                    &nbsp;
                </div>
            </div>
            </div>
            <div style="display:table-cell;">
                <fieldset id="FlsInfBasiProd" runat="server" style="height: 260px; width: 355px;">
                    <legend style="">Información básica</legend>
                    <div class="tabla">
                        <div class="fila">
                            <div class="celda">
                                <asp:Label ID="LblCodProducto" runat="server" CssClass="labels" Text="Código de Producto * "></asp:Label>
                            </div>
                            <div class="celda">
                                <asp:TextBox ID="TxtCodProducto" runat="server" MaxLength="30" Width="195px" Enabled="False"></asp:TextBox>
                            </div>
                        </div>
                        <div class="fila">
                            <div class="celda">
                                <asp:Label ID="LblNomProducto" runat="server" CssClass="labels" Text="Nombre de Producto * "></asp:Label>
                            </div>
                            <div class="celda">
                                <asp:TextBox ID="TxtNomProducto" runat="server" Enabled="False" Width="195px"></asp:TextBox>
                            </div>
                        </div>
                        <div class="fila">
                            <div class="celda">
                                <asp:Label ID="LblAliProducto" runat="server" CssClass="labels" Text="Alias del Producto  "></asp:Label>
                            </div>
                            <div class="celda">
                                <asp:TextBox ID="txtAlias" runat="server" Enabled="False" Width="195px"></asp:TextBox>
                            </div>
                        </div>
                        <div class="fila">
                            <div class="celda">
                                <asp:Label ID="LblInfPrecioVenta" runat="server" CssClass="labels" Text="Precio de Venta:"></asp:Label>
                            </div>
                            <div class="celda">
                                <telerik:radnumerictextbox rendermode="Lightweight" runat="server" id="RadNumericTxtInfPrecioVenta"
                                    emptymessage="--Ingrese Precio--" minvalue="0" width="115px" showspinbuttons="true"
                                    numberformat-decimaldigits="2" maxvalue="100" numberformat-positivepattern="S/. n"
                                    enabled="false"></telerik:radnumerictextbox>
                            </div>
                        </div>
                        <div class="fila">
                            <div class="celda">
                                <asp:Label ID="LblInfPrecioOferta" runat="server" CssClass="labels" Text="Precio de Oferta:"></asp:Label>
                            </div>
                            <div class="celda">
                                <telerik:radnumerictextbox rendermode="Lightweight" runat="server" id="RadNumericTxtInfPrecioOferta"
                                    emptymessage="--Ingrese Precio--" minvalue="0" width="115px" showspinbuttons="true"
                                    numberformat-decimaldigits="2" maxvalue="100" numberformat-positivepattern="S/. n"
                                    enabled="false"></telerik:radnumerictextbox>
                                <br />
                            </div>
                        </div>
                        <div class="fila">
                            <div class="celda">
                                <asp:Label ID="LblInfStock" runat="server" CssClass="labels" Text="Stock"></asp:Label>
                            </div>
                            <div class="celda">
                                <asp:CheckBox ID="CheckBoxInfStock" runat="server" Text="Si queda Stock" CssClass="labels"
                                    Enabled="false"></asp:CheckBox>
                            </div>
                        </div>
                    </div>
                    <div class="tabla">
                    </div>
                    <div class="tabla">
                        <div class="fila">
                            <asp:Label ID="LblInfPromocion" runat="server" CssClass="labels" Text="Promoción "></asp:Label>
                            &nbsp;
                            <asp:TextBox ID="TxtInfPromocion" runat="server" MaxLength="255" Width="333px" Height="70px"
                                Enabled="False" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                </fieldset>
            </div>
        </div>
    </div>
</body>
</html>
